using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.About
{
    public partial class InterestDetail
    {
        public int InterestDetailId { get; set; }

        public string InterestName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int InterestId { get; set; }

        public Interest Interest { get; set; }
    }
}
