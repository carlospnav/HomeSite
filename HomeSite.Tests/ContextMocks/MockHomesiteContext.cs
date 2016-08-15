using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeSite.Models.Repository;
using HomeSiteDomain.Models.About;

namespace HomeSite.Tests.ContextMocks
{
    class MockAboutMeRepository : IAboutMeRepository
    {
        List<Profile> profiles = new List<Profile>() { 
                new Profile() 
                { 
                    ProfileId = 1, 
                    WelcomeText = "Test Welcome Text", 
                    Description = "Test Description", 
                    Interests = 
                    new List<Interest>() 
                    { 
                        new Interest() { InterestId = 1, Name = "Chocolates", ProfileId = 1 }
                    }, 
                    Proficiencies = new List<Proficiency>() 
                    { 
                        new Proficiency() { Name = "Jumping", Description = "Test Jumping", ProficiencyId = 1, ProfileId = 1 }
                    }
                }
        };
        public IEnumerable<Profile> GetAll()
        {
            return profiles;
        }

        public Profile GetSingle(int id)
        {
            return profiles.Where(p => p.ProfileId == id).FirstOrDefault();
        }

        public void Add(Profile entity)
        {
            profiles.Add(entity);
        }

        public void Remove(Profile entity)
        {
            profiles.Remove(entity);
        }

        public void Put(Profile entity, int id)
        {
            //not Implemented.
        }



        public void Dispose()
        {
            //not implemented.
        }
    }
}
