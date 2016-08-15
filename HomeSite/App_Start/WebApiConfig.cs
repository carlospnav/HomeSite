using System.Web.Http;

namespace HomeSite.App_Start
{
    class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            //configuration.Routes.MapHttpRoute(
            //    name: "ApiByNumberOfErrors",
            //    routeTemplate: "api/{controller}/getlast/{numero}",
            //    defaults: new { numero = 5 },
            //    constraints: new { numero = @"\d+" }
            //    );
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}