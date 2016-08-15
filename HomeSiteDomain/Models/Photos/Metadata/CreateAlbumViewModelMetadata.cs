using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeSiteDomain.Models.ValidationAttributes;

namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(CreateAlbumViewModelMetadata))]
    public partial class CreateAlbumViewModel
    {
        internal sealed class CreateAlbumViewModelMetadata
        {
            [DisplayName("Album Name")]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Required]
            public object AlbumName { get; set; }

            [DisplayName("Thumbnail")]
            [HttpPostedFileBaseExtensionValidation]
            [Required]
            public object Thumbnail { get; set; }
        }
    }
}
