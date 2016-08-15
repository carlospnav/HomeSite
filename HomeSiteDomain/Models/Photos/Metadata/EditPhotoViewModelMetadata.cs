using HomeSiteDomain.Models.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(EditPhotoViewModelMetadata))]
    public partial class EditPhotoViewModel
    {
        internal sealed class EditPhotoViewModelMetadata
        {
            [DisplayName("Photo Name")]
            [DataType(DataType.Text)]
            [StringLength(20)]
            public object PhotoName { get; set; }

            [DataType(DataType.MultilineText)]
            [StringLength(200)]
            public object Description { get; set; }


            [DisplayName("Photo File")]
            [HttpPostedFileBaseExtensionValidation(ErrorMessage = "File must be either a jpeg or a png.")]
            [ImageDimensions(ErrorMessage="The photo must be 1200 pixels by 800 pixels either vertically or horizontally.")]
            public object Photo { get; set; }

            [HiddenInput(DisplayValue = false)]
            public object PhotoAbsolutePath { get; set; }

            [HiddenInput(DisplayValue = false)]
            public object Thumbnail { get; set; }

            [HiddenInput(DisplayValue = false)]
            public string ThumbAbsolutePath { get; set; }
        }
    }
}
