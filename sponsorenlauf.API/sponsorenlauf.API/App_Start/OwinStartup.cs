using Microsoft.Owin;
using Owin;
using sponsorenlauf.API;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace sponsorenlauf.API
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();

           
        }
    }
}