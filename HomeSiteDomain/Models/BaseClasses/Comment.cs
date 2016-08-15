using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HomeSiteDomain.Models.Photos;

namespace HomeSiteDomain.Models.BaseClasses
{
    public class Comment
    {
        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(320)]
        public string Body { get; set; }

    }
}
