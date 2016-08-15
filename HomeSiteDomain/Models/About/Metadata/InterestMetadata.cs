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
    [MetadataType(typeof(InterestMetadata))]
    public partial class Interest
    {
        private sealed class InterestMetadata
        {
            [HiddenInput(DisplayValue = false)]
            public object InterestId { get; set; }

            [DisplayName("Interest Name")]
            public object Name { get; set; }

            [DataType(DataType.MultilineText)]
            public object Description { get; set; }

            [HiddenInput(DisplayValue = false)]
            public object ProfileId { get; set; }

            public object Profile { get; set; }
        }
    }
}
