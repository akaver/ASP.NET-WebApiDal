using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;

namespace Web.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IUOW _uow;

        public ArticleController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Article
        public ActionResult Index(string articleName)
        {
            var article = _uow.Articles.FindArticleByName(articleName);
            return View(article);
        }
    }
}