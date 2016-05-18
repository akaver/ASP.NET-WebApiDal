using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources;

namespace Domain
{
    public class Article
    {
        public int ArticleId { get; set; }

        [MaxLength(255)]
        public string ArticleName { get; set; }


        [ForeignKey("ArticleHeadline")]
        [Display(Name = "ArticleHeadline", ResourceType = typeof (Resources.Domain))]
        public int ArticleHeadlineId { get; set; }

        public virtual MultiLangString ArticleHeadline { get; set; }


        [ForeignKey("ArticleBody")]
        [Display(Name = "ArticleBody", ResourceType = typeof (Resources.Domain))]
        public int ArticleBodyId { get; set; }

        public virtual MultiLangString ArticleBody { get; set; }
    }
}