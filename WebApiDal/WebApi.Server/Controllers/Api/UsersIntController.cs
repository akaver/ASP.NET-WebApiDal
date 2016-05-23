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
    [RoutePrefix("api/UsersInt")]
    public class UsersIntController : ApiController
    {
        //private DataBaseContext db = new DataBaseContext();

        private readonly IUOW _uow;

        public UsersIntController(IUOW uow)
        {
            _uow = uow;

        }

        [Route("GetUserByUserName/{userName}")]
        [HttpGet]
        public UserInt GetUserByUserName(string userName)
        {
            return _uow.UsersInt.GetUserByUserName(userName);
        }

        [Route("GetUserByEmail/{email}")]
        [HttpGet]
        public UserInt GetUserByEmail(string email)
        {
            return _uow.UsersInt.GetUserByEmail(email);
        }

        public List<UserInt> GetUsersInt()
        {
            return _uow.UsersInt.All;
        }

        [ResponseType(typeof(UserInt))]
        public IHttpActionResult GetUserInt(int id)
        {
            var userInt = _uow.UsersInt.GetById(id);
            if (userInt == null)
            {
                return NotFound();
            }

            return Ok(userInt);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserInt(int id, UserInt userInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userInt.Id)
            {
                return BadRequest();
            }

            _uow.UsersInt.Update(userInt);

            try
            {
                _uow.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_uow.UsersInt.GetById(id) != null)
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

        [ResponseType(typeof(UserInt))]
        public IHttpActionResult PostUserInt(UserInt userInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.UsersInt.Add(userInt);
            _uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = userInt.Id }, userInt);
        }

        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteUserInt(int id)
        {
            var userInt = _uow.UsersInt.GetById(id);
            if (userInt == null)
            {
                return NotFound();
            }

            _uow.UsersInt.Delete(userInt);
            _uow.Commit();

            return Ok(userInt);
        }

        protected override void Dispose(bool disposing)
        {
        }


    }
}