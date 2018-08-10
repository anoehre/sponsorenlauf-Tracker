using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Timers;
using System.Web.Http;
using dff.Extensions;
using Newtonsoft.Json;
using OfficeOpenXml;
using sponsorenlauf.API.Controllers;

namespace sponsorenlauf.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable Cross Origin Requests
            //config.EnableCors();

            // Web-API-Konfiguration und -Dienste
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var aTimer = new Timer();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        public static List<Laeufer> Laeufer { get; set; }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            WriteToExcel();
            ReadFromExcel();
        }

        public static void ReadFromExcel()
        {
            var laeufer = new List<Laeufer>();
            var existingFile = new FileInfo("C:\\dff\\sponsorenlauf-Tracker\\sponsorenlauf.API\\Sponsoren2.xlsx");
            using (var package = new ExcelPackage(existingFile))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for (var i = 2; i < 650; i++)
                {
                    if (worksheet.Cells[i, 3].Value == null) break;
                    var name = worksheet.Cells[i, 3].Value.ToString();
                    if (string.IsNullOrEmpty(name)) break;
                    if (name == "Läufer1") continue;

                    laeufer.Add(new Laeufer
                    {
                        Runden = worksheet.Cells[i, 1].Value.ToString().TryToInt(),
                        Name = name,
                        Id = i
                    });
                    i += 7;
                }
            }
            Laeufer = laeufer.OrderBy(x => x.Id).ToList();
        }

        private static void WriteToExcel()
        {
            if (Laeufer == null || !Laeufer.Any()) return;
            try
            {
                var existingFile = new FileInfo("C:\\dff\\sponsorenlauf-Tracker\\sponsorenlauf.API\\Sponsoren2.xlsx");
                using (var package = new ExcelPackage(existingFile))
                {
                    var worksheet = package.Workbook.Worksheets[1];

                    Laeufer.ForEach(x => worksheet.Cells[x.Id, 1].Value = x.Runden);

                    worksheet.Calculate();
                    package.SaveAs(existingFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
