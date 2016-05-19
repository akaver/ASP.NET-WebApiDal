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
    [RoutePrefix("api/UsersInt")]
    public class UsersIntController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        private readonly IUOW _uow;

        public UsersIntController(IUOW uow)
        {
            _uow = uow;

        }

        [Route("GetUserByUserName/{userName}")]
        [HttpGet]
        public UserInt FindArticleByName(string userName)
        {
            return _uow.UsersInt.GetUserByUserName(userName);
        }


        // GET: api/Articles
        public IQueryable<UserInt> GetUsersInt()
        {
            return db.UsersInt;
        }

        // GET: api/Articles/5
        [ResponseType(typeof(UserInt))]
        public IHttpActionResult GetUserInt(int id)
        {
            var userInt = db.UsersInt.Find(id);
            if (userInt == null)
            {
                return NotFound();
            }

            return Ok(userInt);
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, UserInt userInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userInt.Id)
            {
                return BadRequest();
            }

            db.Entry(userInt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserIntExists(id))
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
        [ResponseType(typeof(UserInt))]
        public IHttpActionResult PostUserInt(UserInt userInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UsersInt.Add(userInt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userInt.Id }, userInt);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteUserInt(int id)
        {
            var userInt = db.UsersInt.Find(id);
            if (userInt == null)
            {
                return NotFound();
            }

            db.UsersInt.Remove(userInt);
            db.SaveChanges();

            return Ok(userInt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserIntExists(int id)
        {
            return db.UsersInt.Count(e => e.Id == id) > 0;
        }
    }
}