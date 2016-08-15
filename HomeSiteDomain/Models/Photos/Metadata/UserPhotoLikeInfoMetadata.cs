using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(UserPhotoLikeInfoMetadata))]
    public partial class UserPhotoLikeInfo
    {
        sealed internal class UserPhotoLikeInfoMetadata
        {
            public object Vote { get; set; }

            public object PhotoUserId { get; set; }

            public object PhotoUser { get; set; }

            public object PhotoId { get; set; }

            public object Photo { get; set; }
        }
    }
}
