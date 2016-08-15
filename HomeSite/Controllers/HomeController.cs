using HomeSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using HomeSiteDomain.Models.Home;
using HomeSite.Models.Repository;

namespace HomeSite.Controllers
{
    public class HomeController : Controller
    {
        private IHomeRepository context;

        public HomeController(IHomeRepository context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            List<ContentUpdate> contents;
            using (context)
            {
                 contents = context.GetAll().Reverse().Take(6).ToList();
            }
            return View(contents);
        }

        //DELETE AFTER INTEGRATION TESTS IF IT REMAINS UNIMPORTANT.
        //public async Task<ActionResult> CheckClaims()
        //{
        //    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    var claims = await userManager.GetClaimsAsync("0b85d36d-2af4-4c1d-a241-c83b72758c9d");
        //    return View("Index");
        //}

        //CHECK JSON RETURN VALUE FOR HTTPNOTFOUNDRESULT.
        //public ActionResult RedirectToUpdatedContent(string content, string type)
        //{
        //    switch (type)
        //    {
        //        case "Photo":
        //            return RedirectToAction("Index", "Photo", new { content = content });
        //        case "Article":
        //            return RedirectToAction("Index", "Article");
        //        default:
        //            return new HttpNotFoundResult();
        //    }
        //}
    }
}