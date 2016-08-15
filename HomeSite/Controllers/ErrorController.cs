using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HomeSite.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult Index()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
    }
}