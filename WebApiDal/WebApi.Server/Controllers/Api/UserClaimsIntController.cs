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

namespace WebApi.Server.Controllers.Api
{
    [RoutePrefix("api/UserClaimsInt")]
    public class UserClaimsIntController : ApiController
    {
        //private DataBaseContext db = new DataBaseContext();

        private readonly IUOW _uow;

        public UserClaimsIntController(IUOW uow)
        {
            _uow = uow;

        }

        [Route("AllForUserId/{userId}")]
        [HttpGet]
        public List<UserClaimInt> AllForUserId(int userId)
        {
            return _uow.UserClaimsInt.AllForUserId(userId);
        }

        public List<UserClaimInt> GetUserClaimsInt()
        {
            return _uow.UserClaimsInt.All;
        }

        [ResponseType(typeof(UserClaimInt))]
        public IHttpActionResult GetUserClaimInt(int id)
        {
            var userClaimInt = _uow.UserClaimsInt.GetById(id);
            if (userClaimInt == null)
            {
                return NotFound();
            }

            return Ok(userClaimInt);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserClaimInt(int id, UserClaimInt userClaimInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userClaimInt.Id)
            {
                return BadRequest();
            }

            _uow.UserClaimsInt.Update(userClaimInt);

            try
            {
                _uow.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_uow.UserClaimsInt.GetById(id) != null)
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

        [ResponseType(typeof(UserClaimInt))]
        public IHttpActionResult PostUserClaimInt(UserClaimInt userClaimInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.UserClaimsInt.Add(userClaimInt);
            _uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = userClaimInt.Id }, userClaimInt);
        }

        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteUserClaimInt(int id)
        {
            var userClaimInt = _uow.UserClaimsInt.GetById(id);
            if (userClaimInt == null)
            {
                return NotFound();
            }

            _uow.UserClaimsInt.Delete(userClaimInt);
            _uow.Commit();

            return Ok(userClaimInt);
        }

        protected override void Dispose(bool disposing)
        {
        }


    }
}