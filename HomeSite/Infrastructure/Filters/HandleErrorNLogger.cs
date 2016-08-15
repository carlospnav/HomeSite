using HomeSite.Models;
using HomeSiteDomain.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSite.Infrastructure
{
    public class HandleErrorNLogger : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    }
                };
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                var type = filterContext.Exception.GetType();
                ErrorReport error = new ErrorReport { CreatedDate = DateTime.Today, ErrorMessage = filterContext.Exception.Message, ErrorSource = filterContext.Exception.Source, ErrorStackTrace = filterContext.Exception.StackTrace };
                using (var context = new HomeSiteContext())
                {
                    context.Errors.Add(error);
                    context.SaveChanges();
                }
                //Determine Logging Strategy.
                base.OnException(filterContext);
            }
        }
    }
}