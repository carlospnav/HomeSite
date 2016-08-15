using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Photos
{
    public partial class CreatePhotoCommentViewModel
    {
        public int PhotoId { get; set; }

        public string UserName { get; set; }

        public string Body { get; set; }
    }
}
