using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos
{
    [MetadataType(typeof(PhotoCommentMetadata))]
    public partial class PhotoComment
    {
        private sealed class PhotoCommentMetadata 
        {
            public object PhotoCommentId { get; set; }

            public object UserName { get; set; }

            [DataType(DataType.DateTime)]
            public object CreatedDate { get; set; }

            [DataType(DataType.MultilineText)]
            public object Body { get; set; }


            public object PhotoId { get; set; }
            public object Photo { get; set; }
        }
    }
}
