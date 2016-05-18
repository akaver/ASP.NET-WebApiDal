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
using Web.Controllers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MultiLangStringsController : BaseController
    {
        //private DataBaseContext db = new DataBaseContext();
        private readonly IUOW _uow;
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        public MultiLangStringsController(IUOW uow, ILogger logger)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);

            _uow = uow;
        }

        // GET: Admin/MultiLangStrings
        public ActionResult Index()
        {
            return View(_uow.MultiLangStrings.All);
        }

        // GET: Admin/MultiLangStrings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultiLangString multiLangString = _uow.MultiLangStrings.GetById(id);
            if (multiLangString == null)
            {
                return HttpNotFound();
            }
            return View(multiLangString);
        }

        // GET: Admin/MultiLangStrings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MultiLangStrings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MultiLangStringId,Value")] MultiLangString multiLangString)
        {
            if (ModelState.IsValid)
            {
                _uow.MultiLangStrings.Add(multiLangString);
                _uow.Commit();
                return RedirectToAction("Index");
            }

            return View(multiLangString);
        }

        // GET: Admin/MultiLangStrings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultiLangString multiLangString = _uow.MultiLangStrings.GetById(id);
            if (multiLangString == null)
            {
                return HttpNotFound();
            }
            return View(multiLangString);
        }

        // POST: Admin/MultiLangStrings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MultiLangStringId,Value")] MultiLangString multiLangString)
        {
            if (ModelState.IsValid)
            {
                _uow.MultiLangStrings.Update(multiLangString);
                _uow.Commit();

                return RedirectToAction("Index");
            }
            return View(multiLangString);
        }

        // GET: Admin/MultiLangStrings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultiLangString multiLangString = _uow.MultiLangStrings.GetById(id);
            if (multiLangString == null)
            {
                return HttpNotFound();
            }
            return View(multiLangString);
        }

        // POST: Admin/MultiLangStrings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var multiLangString = _uow.MultiLangStrings.GetById(id);
            foreach (var translation in multiLangString.Translations.ToList())
            {
                _uow.Translations.Delete(translation);
            }
            _uow.MultiLangStrings.Delete(multiLangString);
            _uow.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Debug("InstanceId: " + _instanceId + " Disposing:" + disposing);

            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}