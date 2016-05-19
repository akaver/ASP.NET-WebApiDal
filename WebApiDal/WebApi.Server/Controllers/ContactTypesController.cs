using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Domain;

namespace WebApi.Server.Controllers
{
    public class ContactTypesController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: api/ContactTypes
        public IQueryable<ContactType> GetContactTypes()
        {
            return db.ContactTypes;
        }

        // GET: api/ContactTypes/5
        [ResponseType(typeof(ContactType))]
        public IHttpActionResult GetContactType(int id)
        {
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return NotFound();
            }

            return Ok(contactType);
        }

        // PUT: api/ContactTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactType(int id, ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactType.ContactTypeId)
            {
                return BadRequest();
            }

            db.Entry(contactType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ContactTypes
        [ResponseType(typeof(ContactType))]
        public IHttpActionResult PostContactType(ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactTypes.Add(contactType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactType.ContactTypeId }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [ResponseType(typeof(ContactType))]
        public IHttpActionResult DeleteContactType(int id)
        {
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return NotFound();
            }

            db.ContactTypes.Remove(contactType);
            db.SaveChanges();

            return Ok(contactType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactTypeExists(int id)
        {
            return db.ContactTypes.Count(e => e.ContactTypeId == id) > 0;
        }
    }
}