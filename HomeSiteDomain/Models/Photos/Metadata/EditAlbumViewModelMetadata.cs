using HomeSiteDomain.Models.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(EditViewModelMetadata))]
    public partial class EditAlbumViewModel
    {
        internal sealed class EditViewModelMetadata
        {
            [DisplayName("Album Name")]
            [StringLength(20)]
            [DataType(DataType.Text)]
            [Required]
            public object AlbumName { get; set; }

            [DisplayName("Thumbnail")]
            [HttpPostedFileBaseExtensionValidation]
            public object Thumbnail { get; set; }
        }
    }
}
