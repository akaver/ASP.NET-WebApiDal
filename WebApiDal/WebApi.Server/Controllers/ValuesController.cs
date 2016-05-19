using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Interfaces;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;

namespace WebApi.Server.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        private readonly IUOW _uow;

        public ValuesController(IUOW uow)
        {
            _uow = uow;
        }


        // GET api/values
        public string Get()
        {
            return DateTime.Now.ToShortTimeString() + " " + _uow.Persons.All.Count;
        }

        [Authorize]
        [Route("Authorized")]
        [HttpGet]
        public string Authorized()
        {
            return DateTime.Now.ToShortTimeString() + " " + User.Identity.Name+" "+User.Identity.GetUserId<int>();
        }
    }
}
