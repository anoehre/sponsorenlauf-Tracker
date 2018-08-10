using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
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

        [HttpGet]
        [Route("rundeIncrease/{laeuferId}/{newValue}")]
        public IHttpActionResult RundeIncrease([FromUri] int laeuferId, int newValue)
        {
            Write(laeuferId, newValue);

            var laeuferListBuild = LaeuferListBuild();

            var xxx = new DashboardHub();
            xxx.SendMessageViaSignalR();
            
            return Ok(laeuferListBuild);

        }




        private List<Laeufer> LaeuferListBuild()
        {
            if (WebApiConfig.Laeufer == null)
            {
                WebApiConfig.ReadFromExcel();
            }
            return WebApiConfig.Laeufer;
        }

        private void Write(int row, int newValue)
        {
            WebApiConfig.Laeufer.First(x => x.Id == row).Runden = newValue;
        }

       
    }
}