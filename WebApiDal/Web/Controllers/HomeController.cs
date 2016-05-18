using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUOW _uow;

        public HomeController(IUOW uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            var vm = new HomeIndexViewModel()
            {
                Article = _uow.Articles.FindArticleByName("HomeIndex")
            };
            return View(vm);
        }


        public ActionResult ApiDemo()
        {
            return View();
        }

    }
}