using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HomeSiteDomain.Models.BaseClasses
{
    public abstract class PhotoViewModel
    {
        public string PhotoName { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase Photo { get; set; }

        public string PhotoAbsolutePath { get; set; }

        public Image Thumbnail { get; set; }

        public string ThumbAbsolutePath { get; set; }
    }
}
