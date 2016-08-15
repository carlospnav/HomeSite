using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeSiteDomain.Models.Photos
{
    public partial class Album
    {
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }
        
        public string ThumbnailUrl { get; set; }

        public virtual List<Photo> Photos { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Album album = obj as Album;
            if (album == null)
                return false;

            return (AlbumId == album.AlbumId) && (AlbumName == album.AlbumName) && (ThumbnailUrl == album.ThumbnailUrl) && (Photos.Count == album.Photos.Count);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}