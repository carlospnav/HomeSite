using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HomeSite.Hubs
{
    public class PhotoCommentsHub : Hub
    {
        public void Send(string name, string message, int photoId, string pageToUpdate)
        {
            var clientId = Context.ConnectionId;
            Clients.Caller.sendClient(name, message, pageToUpdate);
            Clients.AllExcept(new string[] { clientId }).sendOthers(photoId, pageToUpdate);
        }
    }
}