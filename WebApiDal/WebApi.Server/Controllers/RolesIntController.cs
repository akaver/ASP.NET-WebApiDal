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
using Domain.Identity;
using Interfaces.UOW;

namespace WebApi.Server.Controllers
{
    [RoutePrefix("api/RolesInt")]
    public class RolesIntController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        private readonly IUOW _uow;

        public RolesIntController(IUOW uow)
        {
            _uow = uow;
        }

        [Route("GetRolesForUser/{userId}")]
        [HttpGet]
        public List<RoleInt> GetRolesForUser(int userId)
        {
            return _uow.RolesInt.GetRolesForUser(userId);
        }


        // GET: api/Articles
        public IQueryable<RoleInt> GetRolesInt()
        {
            return db.RolesInt;
        }

        // GET: api/Articles/5
        [ResponseType(typeof(RoleInt))]
        public IHttpActionResult GetRoleInt(int id)
        {
            var RoleInt = db.RolesInt.Find(id);
            if (RoleInt == null)
            {
                return NotFound();
            }

            return Ok(RoleInt);
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, RoleInt roleInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roleInt.Id)
            {
                return BadRequest();
            }

            db.Entry(roleInt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleIntExists(id))
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

        // POST: api/Articles
        [ResponseType(typeof(RoleInt))]
        public IHttpActionResult PostRoleInt(RoleInt roleInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RolesInt.Add(roleInt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = roleInt.Id }, roleInt);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteRoleInt(int id)
        {
            var roleInt = db.RolesInt.Find(id);
            if (roleInt == null)
            {
                return NotFound();
            }

            db.RolesInt.Remove(roleInt);
            db.SaveChanges();

            return Ok(roleInt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleIntExists(int id)
        {
            return db.RolesInt.Count(e => e.Id == id) > 0;
        }
    }
}