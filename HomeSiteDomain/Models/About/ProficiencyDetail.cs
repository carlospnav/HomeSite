using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.About
{
    public partial class ProficiencyDetail
    {
        public int ProficiencyDetailId { get; set; }

        public string ProficiencyName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int ProficiencyId { get; set; }

        public Proficiency Proficiency { get; set; }
    }
}
