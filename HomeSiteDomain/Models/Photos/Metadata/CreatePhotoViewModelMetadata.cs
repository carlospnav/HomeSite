using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeSiteDomain.Models.ValidationAttributes;
using System.Web.Mvc;


namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(CreatePhotoViewModelMetadata))]
    public partial class CreatePhotoViewModel
    {
        internal sealed class CreatePhotoViewModelMetadata
        {
            [DisplayName("Photo Name")]
            [DataType(DataType.Text)]
            [StringLength(20)]
            public object PhotoName { get; set; }

            [DataType(DataType.MultilineText)]
            [StringLength(200)]
            public object Description { get; set; }
            
            [DisplayName("Photo File")]
            [HttpPostedFileBaseExtensionValidation(ErrorMessage="File must be either a jpeg or a png.")]
            [ImageDimensions(ErrorMessage="The photo must be 1200 pixels by 800 pixels either vertically or horizontally.")]
            [Required]
            public object Photo { get; set; }

            [HiddenInput(DisplayValue=false)]
            public object PhotoAbsolutePath { get; set; }

            [HiddenInput(DisplayValue = false)]
            public object Thumbnail { get; set; }

            [HiddenInput(DisplayValue = false)]
            public string ThumbAbsolutePath { get; set; }
        }
    }
}
