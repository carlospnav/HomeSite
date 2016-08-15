using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeSiteDomain.Models.About
{
    [MetadataType(typeof(ProfileMetadata))]
    public partial class Profile
    {
        private sealed class ProfileMetadata
        {
            [HiddenInput(DisplayValue = false)]
            public object ProfileId { get; set; }

            [DataType(DataType.MultilineText)]
            public object WelcomeText { get; set; }

            [DataType(DataType.MultilineText)]
            public object Description { get; set; }

            public object Interests { get; set; }

            public object Proficiencies { get; set; }
        }
    }
}
