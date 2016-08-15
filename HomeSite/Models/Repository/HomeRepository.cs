using HomeSiteDomain.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSite.Models.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private HomeSiteContext context;

        public HomeRepository()
        {
            context = new HomeSiteContext();
        }

        public IEnumerable<ContentUpdate> GetAll()
        {
            return context.ContentUpdates.ToList();
        }

        public ContentUpdate GetSingle(int Id)
        {
            using (context)
            {
                return context.ContentUpdates.Find(Id);
            }
        }

        public void Add(ContentUpdate entity)
        {
            context.ContentUpdates.Add(entity);
            context.SaveChanges();
        }

        public void Remove(ContentUpdate entity)
        {
            context.ContentUpdates.Remove(entity);
            context.SaveChanges();
        }

        public void Put(ContentUpdate entity, int id)
        {
            ContentUpdate content = GetSingle(id);
            content.Content = entity.Content;
            content.Name = entity.Name;
            content.Type = entity.Type;
            content.RouteValue = entity.RouteValue;
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}