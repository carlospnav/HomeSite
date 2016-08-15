using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(PhotoMetadata))]
    public partial class Photo
    {
        private sealed class PhotoMetadata 
        {
            public int PhotoId { get; set; }

            [DisplayName("Photo Name")]
            [DataType(DataType.Text)]
            public object PhotoName { get; set; }

            [DisplayName("Creation Date")]
            [DataType(DataType.Date)]
            public object CreatedDate { get; set; }

            [DataType(DataType.MultilineText)]
            public object Description { get; set; }

            [DisplayName("Thumbnail File")]
            [DataType(DataType.ImageUrl)]
            public object PhotoThumbnailUrl { get; set; }

            [DisplayName("Photo File")]
            [DataType(DataType.ImageUrl)]
            public object PhotoUrl { get; set; }

            public object Orientation { get; set; }

            public object LikeCount { get; set; }

            public object UserPhotoLikeInfo { get; set; }

            public object AlbumId { get; set; }
            public object Album { get; set; }

            public object Comments { get; set; }
        }
    }
}
