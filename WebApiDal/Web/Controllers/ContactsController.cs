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
using Microsoft.AspNet.Identity;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class ContactsController : BaseController
    {
        private readonly IUOW _uow;
 
        public ContactsController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Contacts
        public ActionResult Index()
        {
            var vm = new ContactIndexViewModel()
            {
                Contacts = _uow.Contacts.AllforUser(User.Identity.GetUserId<int>())
            };
            vm.ContactCount = vm.Contacts.Count;

            return View(vm);
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = _uow.Contacts.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var vm = new ContactCreateEditViewModel();
            vm.ContactTypeSelectList = new SelectList(_uow.ContactTypes.All.Select(t => new {t.ContactTypeId, ContactTypeName = t.ContactTypeName.Translate()}).ToList(), nameof(ContactType.ContactTypeId), nameof(ContactType.ContactTypeName));
            vm.PersonSelectList = new SelectList(_uow.Persons.GetAllForUser(User.Identity.GetUserId<int>()), nameof(Person.PersonId), nameof(Person.FirstLastname));
            return View(vm);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Contacts.Add(vm.Contact);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }

            vm.ContactTypeSelectList = new SelectList(_uow.ContactTypes.All.Select(t => new { t.ContactTypeId, ContactTypeName = t.ContactTypeName.Translate() }).ToList(), nameof(ContactType.ContactTypeId), nameof(ContactType.ContactTypeName), vm.Contact.ContactTypeId);
            vm.PersonSelectList = new SelectList(_uow.Persons.GetAllForUser(User.Identity.GetUserId<int>()), nameof(Person.PersonId), nameof(Person.FirstLastname), vm.Contact.PersonId);
            return View(vm);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = _uow.Contacts.GetForUser(id.Value, User.Identity.GetUserId<int>());
            if (contact == null)
            {
                return HttpNotFound();
            }
            // ReSharper disable once UseObjectOrCollectionInitializer
            var vm = new ContactCreateEditViewModel();
            vm.Contact = contact;
            vm.ContactTypeSelectList = new SelectList(_uow.ContactTypes.All.Select(t => new { t.ContactTypeId, ContactTypeName = t.ContactTypeName.Translate() }).ToList(), nameof(ContactType.ContactTypeId), nameof(ContactType.ContactTypeName), vm.Contact.ContactTypeId);
            vm.PersonSelectList = new SelectList(_uow.Persons.GetAllForUser(User.Identity.GetUserId<int>()), nameof(Person.PersonId), nameof(Person.FirstLastname), vm.Contact.PersonId);

            return View(vm);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Contacts.Update(vm.Contact);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            vm.ContactTypeSelectList = new SelectList(_uow.ContactTypes.All.Select(t => new { t.ContactTypeId, ContactTypeName = t.ContactTypeName.Translate() }).ToList(), nameof(ContactType.ContactTypeId), nameof(ContactType.ContactTypeName), vm.Contact.ContactTypeId);
            vm.PersonSelectList = new SelectList(_uow.Persons.GetAllForUser(User.Identity.GetUserId<int>()), nameof(Person.PersonId), nameof(Person.FirstLastname), vm.Contact.PersonId);
            return View(vm);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = _uow.Contacts.GetForUser(id.Value, User.Identity.GetUserId<int>());
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Contacts.Delete(id);
            _uow.Commit();
            return RedirectToAction(nameof(Index));
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
