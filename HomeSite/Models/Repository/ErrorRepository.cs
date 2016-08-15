using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeSiteDomain.Models.BaseClasses;

namespace HomeSite.Models.Repository
{
    public class ErrorRepository : IErrorRepository
    {
        private HomeSiteContext context;
        public ErrorRepository()
        {
            this.context = new HomeSiteContext();
        }

        public IEnumerable<ErrorReport> GetAll()
        {
            return context.Errors.ToList();
        }

        public ErrorReport GetSingle(int Id)
        {
            return context.Errors.Find(Id);
        }

        public IEnumerable<ErrorReport> GetLastX(int numberOfErrors)
        {
            List<ErrorReport> errors = context.Errors.ToList();
            errors.Reverse();
            return errors.Take(numberOfErrors);
        }

        public void Add(ErrorReport entity)
        {
            context.Errors.Add(entity);
            context.SaveChanges();
        }

        public void Remove(ErrorReport entity)
        {
            context.Errors.Remove(entity);
            context.SaveChanges();
        }

        public void Put(ErrorReport entity, int id)
        {
            ErrorReport error = context.Errors.Find(id);
            error.ErrorMessage = entity.ErrorMessage;
            error.ErrorSource = entity.ErrorSource;
            error.ErrorStackTrace = entity.ErrorStackTrace;
            context.SaveChanges();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}