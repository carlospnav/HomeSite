using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HomeSiteDomain.Models.Photos
{
    public abstract class AlbumViewModel
    {
        public string AlbumName { get; set; }

        public HttpPostedFileBase Thumbnail { get; set; }
    }
}
