using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.Home
{
    public class ContentUpdate
    {
        public int ContentUpdateId { get; set; }

        public ContentType Type { get; set; }

        public DateTime DateCreated{ get; set; }

        public string RouteValue { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }

    public enum ContentType
    {
        Photo = 0,
        Profile = 1,
        Article = 2
    }
}
