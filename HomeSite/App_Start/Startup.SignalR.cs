using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSite
{
    public partial class Startup
    {
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}