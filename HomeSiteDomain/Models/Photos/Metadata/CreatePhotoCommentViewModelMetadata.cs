using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos.Metadata
{
    [MetadataType(typeof(CreatePhotoCommentViewModelMetadata))]
    public partial class CreatePhotoCommentViewModel
    {
        internal sealed class CreatePhotoCommentViewModelMetadata
        {
            public object PhotoId { get; set; }

            public object UserName { get; set; }

            public string Body { get; set; }
        }
    }
}
