using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace sponsorenlauf.API.Controllers
{
    public class Laeufer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Runden { get; set; }
    }
}
