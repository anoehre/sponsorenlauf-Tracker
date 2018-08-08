using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using dff.Extensions;
using OfficeOpenXml;

namespace sponsorenlauf.API.Controllers
{
    [RoutePrefix("v1/mobile")]
    [EnableCors("*", "*", "*")]
    public class MobileSiteController : ApiController
    {
        [HttpGet]
        [Route("laeuferList")]
        public IHttpActionResult LaeuferListGet()
        {
            return Ok(LaeuferListBuild());
        }

        private static List<Laeufer> LaeuferListBuild()
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

                    laeufer.Add(new Laeufer
                    {
                        Runden = worksheet.Cells[i, 1].Value.ToString().TryToInt(),
                        Name = name,
                        Id = i
                    });
                    i += 7;
                }
            }

            return laeufer;
        }

        [HttpGet]
        [Route("rundeIncrease/{laeuferId}/{newValue}")]
        public IHttpActionResult RundeIncrease([FromUri] int laeuferId, int newValue)
        {
            Write(laeuferId, newValue);
            return Ok(LaeuferListBuild());
        }

        private static void Write(int row, int newValue)
        {
            var existingFile = new FileInfo("C:\\dff\\sponsorenlauf-Tracker\\sponsorenlauf.API\\Sponsoren2.xlsx");
            using (var package = new ExcelPackage(existingFile))
            {
                var worksheet = package.Workbook.Worksheets[1];
                worksheet.Cells[row, 1].Value = newValue;
                worksheet.Calculate();
                package.SaveAs(existingFile);
            }
        }
    }
}