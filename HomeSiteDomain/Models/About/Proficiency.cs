using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeSiteDomain.Models.About
{
    public partial class Proficiency
    {
        public int ProficiencyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int ProfileId { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual List<ProficiencyDetail> ProficiencyDetails { get; set; }
    }
}