using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos
{
    public partial class UserPhotoLikeInfo
    {
        public bool Vote { get; set; }

        public string PhotoUserId { get; set; }
        public PhotoUser PhotoUser { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}
