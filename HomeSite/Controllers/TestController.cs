using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSite.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var x = 0;

            x /= x;
            return View();
        }

        public ActionResult TesteException()
        {
            throw new Exception("oi");
        }

        public ActionResult Test500()
        {
            throw new HttpException(500, "Server stuff");
        }

        public ActionResult Test404()
        {
            throw new HttpException(404, "File not sajsas");
        }

    }
}