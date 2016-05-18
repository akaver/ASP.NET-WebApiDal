using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Domain;
using NLog;
using Web.Areas.Admin.ViewModels;
using Web.Controllers;
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    public class ContactTypesController : BaseController
    {

        private readonly IUOW _uow;

        public ContactTypesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/ContactTypes
        public ActionResult Index()
        {
            var vm = _uow.ContactTypes.All;
            return View(vm);
        }

        // GET: Admin/ContactTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = _uow.ContactTypes.GetById(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // GET: Admin/ContactTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ContactTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.ContactType == null) vm.ContactType = new ContactType();
                
                vm.ContactType.ContactTypeName = new MultiLangString(vm.ContactTypeName,
                    CultureHelper.GetCurrentNeutralUICulture(), vm.ContactTypeName,
                    nameof(vm.ContactType) + "." + vm.ContactType.ContactTypeId + "." + nameof(vm.ContactType.ContactTypeName));

                _uow.ContactTypes.Add(vm.ContactType);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);

        }

        // GET: Admin/ContactTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = _uow.ContactTypes.GetById(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            var vm = new ContactTypeCreateEditViewModel()
            {
                ContactType = contactType,
                ContactTypeName = contactType.ContactTypeName.Translate()
            };
            return View(vm);
        }

        // POST: Admin/ContactTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.ContactType.ContactTypeName = _uow.MultiLangStrings.GetById(vm.ContactType.ContactTypeNameId);
                vm.ContactType.ContactTypeName.SetTranslation(vm.ContactTypeName, CultureHelper.GetCurrentNeutralUICulture(),
                    nameof(vm.ContactType) + "." + vm.ContactType.ContactTypeId + "." + nameof(vm.ContactType.ContactTypeName));

                _uow.ContactTypes.Update(vm.ContactType);
                _uow.Commit();
                return RedirectToAction(nameof(Index));

            }
            return View(vm);
        }

        // GET: Admin/ContactTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = _uow.ContactTypes.GetById(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // POST: Admin/ContactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.ContactTypes.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
