using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GuiaNet
{
    public class HubMSN : Hub
    {
        public void Send(string message)
        {
            var id = Context.ConnectionId.Substring(0,-4);

            Clients.All.receive(id, message);
        }
    }
}