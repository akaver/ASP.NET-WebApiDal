using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class ArticleIndexViewModel
    {
        public List<Article> Articles { get; set; }
        public bool ViewHtml { get; set; }
    }

    public class ArticleViewModel
    {
        public Article Article { get; set; }

        [Display(Name = "ArticleHeadline", ResourceType = typeof (Resources.Domain))]
        [AllowHtml]
        [MaxLength(255)]
        public string ArticleHeadline { get; set; }

        [Display(Name = "ArticleBody", ResourceType = typeof (Resources.Domain))]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [MaxLength(40960)]
        public string ArticleBody { get; set; }
    }
}