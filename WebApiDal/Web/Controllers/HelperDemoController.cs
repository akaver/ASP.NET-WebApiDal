using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DAL;
using DAL.Helpers;
using DAL.Interfaces;
using Domain;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HelperDemoController : Controller
    {

        private readonly IUOW _uow;

        // manually initialize uow
        //private readonly IUOW _uow = new 
        //    UOW(
        //    new EFRepositoryProvider(
        //        new EFRepositoryFactories(NLog.LogManager.GetCurrentClassLogger()), NLog.LogManager.GetCurrentClassLogger()), 
        //    new DataBaseContext(), NLog.LogManager.GetCurrentClassLogger());

        public HelperDemoController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: HelperDemo
        public ActionResult Index()
        {
            var vm = new HelperDemoIndexViewModel()
            {
                DropDownList = new SelectList(_uow.Persons.All,nameof(Person.PersonId),nameof(Person.Firstname)),
                ListBoxList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.Firstname)),

                RadioButtonList1 = _uow.Persons.All.Where(p => p.Firstname.StartsWith("A")).Take(5).ToList(),
                RadioButtonList2 = _uow.Persons.All.Where(p => p.Firstname.StartsWith("B")).Take(5).ToList(),


            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HelperDemoIndexViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //save to db, redirect to some other view
            }

            vm.DropDownList = new SelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.Firstname), vm.DropDownListId);
            vm.ListBoxList = new SelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.Firstname), vm.DropDownListId);
            vm.RadioButtonList1 = _uow.Persons.All.Where(p => p.Firstname.StartsWith("A")).Take(5).ToList();
            vm.RadioButtonList2 = _uow.Persons.All.Where(p => p.Firstname.StartsWith("B")).Take(5).ToList();


            return View(vm);
        }


    }
}