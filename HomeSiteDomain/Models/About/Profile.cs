using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSiteDomain.Models.About
{
    public partial class Profile
    {
        public int ProfileId { get; set; }

        public string WelcomeText { get; set; }

        public string Description { get; set; }

        public virtual List<Interest> Interests { get; set; }

        public virtual List<Proficiency> Proficiencies { get; set; }
    }
}