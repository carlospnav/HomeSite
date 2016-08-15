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
    [MetadataType(typeof(InterestDetailMetadata))]
    public partial class InterestDetail
    {
        private sealed class InterestDetailMetadata
        {
            [HiddenInput(DisplayValue=false)]
            public object InterestDetailId { get; set; }

            [DisplayName("Name")]
            public object InterestName { get; set; }

            [DataType(DataType.MultilineText)]
            public object Description { get; set; }

            [DataType(DataType.ImageUrl)]
            public object ImageUrl { get; set; }

            [HiddenInput(DisplayValue = false)]
            public object InterestId { get; set; }

            public object Interest { get; set; }
        }
    }
}
