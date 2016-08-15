using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HomeSiteDomain.Models;

namespace HomeSiteDomain.Models.Photos
{
    public partial class Photo
    {
        public int PhotoId { get; set; }

        public string PhotoName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        public string PhotoThumbnailUrl { get; set; }

        public string PhotoUrl { get; set; }

        public string Orientation { get; set; }

        public int LikeCount { get; set; }

        public virtual List<UserPhotoLikeInfo> UserPhotoLikeInfo { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public virtual List<PhotoComment> Comments { get; set; }
    }
}