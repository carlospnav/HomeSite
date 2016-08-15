using HomeSiteDomain.Models.About;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeSiteDomain.Models.About
{
	[MetadataType(typeof(Proficiency.ProficiencyMetadata))]
    public partial class Proficiency
    {
        internal sealed class ProficiencyMetadata
        {
			[HiddenInput(DisplayValue = false)]
            public Object ProficiencyId { get; set; }
			
			[DisplayName("Proficiency Name")]
            public Object Name { get; set; }

			[DataType(DataType.MultilineText)]
            public Object Description { get; set; }

            [HiddenInput(DisplayValue = false)]
            public Object ProfileId { get; set; }

            public Object Profile { get; set; }

        }
    }
}
