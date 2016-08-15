using HomeSite.Models.Repository;
using HomeSiteDomain.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace HomeSite.Controllers.API
{
    [RoutePrefix("api/error")]
    public class ErrorController : ApiController
    {
        private IErrorRepository context;
        public ErrorController(IErrorRepository context)
        {
            this.context = context;
        }

        [Route("getall")]
        [HttpGet]
        public IEnumerable<ErrorReport> GetAll()
        {
            using (context)
            {
                return context.GetAll();
            }
        }
        
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            ErrorReport error;
            using (context)
            {
                error = context.GetSingle(id);
            }

            if (error == null)
            {
                return NotFound();
            }
            else
                return Ok(error);
        }

        [Route("getlast/{id:int?}")]
        [HttpGet]
        public IEnumerable<ErrorReport> GetLast(int numero = 5)
        {
            using (context)
            {
                return context.GetLastX(numero);
            }

        }

        [HttpPost]
        public void PostError(ErrorReport error)
        {
            using (context)
            {
                context.Add(error);
            }
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteError(int id)
        {
            ErrorReport error;
            using (context)
            {
                error = context.GetSingle(id);
                if (error == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Remove(error);
                    return Ok();
                }
            }
        }
    }
}
