using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace HomeSiteDomain.Models.ValidationAttributes
{
    class ImageDimensionsAttribute : ValidationAttribute
    {
        public ImageDimensionsAttribute() { }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                Image img;
                img = Image.FromStream(file.InputStream, true, true);
                var height = img.Height;
                var width = img.Width;
                if ((height == 800 && width == 1200) || (height == 1200 && width == 800)) { return true; }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
