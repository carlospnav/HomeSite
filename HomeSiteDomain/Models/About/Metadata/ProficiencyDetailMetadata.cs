using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeSiteDomain.Models.About.Metadata
{
    [MetadataType(typeof(ProficiencyDetailMetadata))]
    public partial class ProficiencyDetail
    {
        private sealed class ProficiencyDetailMetadata
        {
            [HiddenInput(DisplayValue=false)]
            public object ProficiencyDetailId { get; set; }

            [DisplayName("Name")]
            public string ProficiencyName { get; set; }

            [DataType(DataType.MultilineText)]
            public string Description { get; set; }

            [DataType(DataType.ImageUrl)]
            public string ImageUrl { get; set; }

            [HiddenInput(DisplayValue=false)]
            public object ProficiencyId { get; set; }

            public object Proficiency { get; set; }
        }
    }
}
