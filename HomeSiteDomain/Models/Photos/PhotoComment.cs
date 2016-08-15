using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeSiteDomain.Models.BaseClasses;

namespace HomeSiteDomain.Models.Photos
{
    public partial class PhotoComment : Comment
    {
        public int PhotoCommentId { get; set; }

        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}