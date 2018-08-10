using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace sponsorenlauf.API
{
    public class DashboardHub : Hub
    {
        public bool SendMessageViaSignalR()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();
            hubContext.Clients.All.hello("");
            return true;
        }

        [HubMethodName("Register")]
        public void Register(string dashoboardToken, string connectionId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();
            Groups.Add(connectionId, "sposorenlauf");
        }
    }
}