using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeSiteDomain.Models.ValidationAttributes
{
    public class HttpPostedFileBaseExtensionValidationAttribute : ValidationAttribute
    {
        public HttpPostedFileBaseExtensionValidationAttribute()
        {

        }
        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            
            if (file != null)
            {
                if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }
    }
}