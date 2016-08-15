using HomeSiteDomain.Models.About;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HomeSite.Models.Repository
{
    public class AboutmeRepository : IAboutMeRepository
    {
        private HomeSiteContext context;

        public AboutmeRepository()
        {
            this.context = new HomeSiteContext();
        }

        public IEnumerable<Profile> GetAll()
        {
            return context.Profiles.ToList();
        }

        public Profile GetSingle(int id = 0)
        {
            if (id == 0)
            {
                return context.Profiles.FirstOrDefault();
            }
            return context.Profiles.Where(x => x.ProfileId == id).Include(x => x.Interests.Select(n => n.InterestDetails)).Include(x => x.Proficiencies.Select(n => n.ProficiencyDetails)).FirstOrDefault();
        }

        public void Add(Profile entity)
        {
            Profile profile = context.Profiles.Add(entity);
            context.SaveChanges();
        }

        public void Remove(Profile profile)
        {
            context.Profiles.Remove(profile);
            context.SaveChanges();
        }

        public void Put(Profile profile, int profileId)
        {
            //Profile entity = context.Profiles.FirstOrDefault();
            //entity.Interests.Find()
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}