using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeSite.Startup))]
namespace HomeSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureSignalR(app);
        }
    }
}
