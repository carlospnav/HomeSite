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
    }
}