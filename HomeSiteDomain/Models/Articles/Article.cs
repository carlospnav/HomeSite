using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeSiteDomain.Models.BaseClasses;

namespace HomeSiteDomain.Models.Articles
{
    public class Article
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        public List<Comment> Comments { get; set; }
    }
}