using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(AlbumMetadata))]
    public partial class Album
    {
        private sealed class AlbumMetadata
        {
            public object AlbumId { get; set; }
            
            [DisplayName("Album Name")]
            [DataType(DataType.Text)]
            [Required]
            public object AlbumName { get; set; }

            [DisplayName("Thumnail Photo")]
            [DataType(DataType.ImageUrl)]
            [Required]
            public object ThumbnailUrl { get; set; }

            public  object Photos { get; set; }
        }
    }
}
