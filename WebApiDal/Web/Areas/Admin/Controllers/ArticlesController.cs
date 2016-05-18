using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Domain;
using Web.Areas.Admin.ViewModels;
using Web.Controllers;
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticlesController : BaseController
    {
        //private readonly DataBaseContext _db = new DataBaseContext();
        private readonly IUOW _uow;

        public ArticlesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/Articles
        public ActionResult Index(bool viewHtml = false)
        {
            var vm = new ArticleIndexViewModel()
            {
                Articles = _uow.Articles.AllWithTranslations(),
                ViewHtml = viewHtml
            };
            return View(vm);
        }

        // GET: Admin/Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _uow.Articles.GetById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Article.ArticleHeadline = new MultiLangString(vm.ArticleHeadline,
                    CultureHelper.GetCurrentNeutralUICulture(), vm.ArticleHeadline,
                    nameof(vm.Article) + "." + vm.Article.ArticleId + "." + nameof(vm.Article.ArticleHeadline));
                vm.Article.ArticleBody = new MultiLangString(vm.ArticleBody, CultureHelper.GetCurrentNeutralUICulture(),
                    vm.ArticleBody,
                    nameof(vm.Article) + "." + vm.Article.ArticleId + "." + nameof(vm.Article.ArticleBody));

                _uow.Articles.Add(vm.Article);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _uow.Articles.GetById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            var vm = new ArticleViewModel
            {
                Article = article,
                ArticleHeadline = article.ArticleHeadline.Translate(),
                ArticleBody = article.ArticleBody.Translate()
            };
            return View(vm);
        }

        // POST: Admin/Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Article.ArticleHeadline = _uow.MultiLangStrings.GetById(vm.Article.ArticleHeadlineId);
                vm.Article.ArticleHeadline.SetTranslation(vm.ArticleHeadline, CultureHelper.GetCurrentNeutralUICulture(),
                    nameof(vm.Article) + "." + vm.Article.ArticleId + "." + nameof(vm.Article.ArticleHeadline));

                vm.Article.ArticleBody = _uow.MultiLangStrings.GetById(vm.Article.ArticleBodyId);
                vm.Article.ArticleBody.SetTranslation(vm.ArticleBody, CultureHelper.GetCurrentNeutralUICulture(),
                    nameof(vm.Article) + "." + vm.Article.ArticleId + "." + nameof(vm.Article.ArticleBody));

                _uow.Articles.Update(vm.Article);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _uow.Articles.GetById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Articles.DeleteArticleWithTranslations(id);
            _uow.Commit();

            return RedirectToAction(nameof(Index));
        }
    }
}