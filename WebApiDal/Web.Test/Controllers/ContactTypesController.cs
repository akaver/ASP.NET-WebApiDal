using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Domain;

namespace Web.Test.Controllers
{
    public class ContactTypesController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: ContactTypes
        public ActionResult Index()
        {
            var contactTypes = db.ContactTypes.Include(c => c.ContactTypeName);
            return View(contactTypes.ToList());
        }

        // GET: ContactTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // GET: ContactTypes/Create
        public ActionResult Create()
        {
            ViewBag.ContactTypeNameId = new SelectList(db.MultiLangStrings, "MultiLangStringId", "Value");
            return View();
        }

        // POST: ContactTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactTypeId,ContactTypeNameId,CreatedAtDT,CreatedBy,ModifiedAtDT,ModifiedBy")] ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                db.ContactTypes.Add(contactType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactTypeNameId = new SelectList(db.MultiLangStrings, "MultiLangStringId", "Value", contactType.ContactTypeNameId);
            return View(contactType);
        }

        // GET: ContactTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactTypeNameId = new SelectList(db.MultiLangStrings, "MultiLangStringId", "Value", contactType.ContactTypeNameId);
            return View(contactType);
        }

        // POST: ContactTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactTypeId,ContactTypeNameId,CreatedAtDT,CreatedBy,ModifiedAtDT,ModifiedBy")] ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactTypeNameId = new SelectList(db.MultiLangStrings, "MultiLangStringId", "Value", contactType.ContactTypeNameId);
            return View(contactType);
        }

        // GET: ContactTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // POST: ContactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactType contactType = db.ContactTypes.Find(id);
            db.ContactTypes.Remove(contactType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
