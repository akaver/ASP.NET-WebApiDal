using System;
using System.Web.Http;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Web.Controllers.Api
{
    [RoutePrefix("api/Values")]
    [Authorize]
    public class ValuesController : ApiController
    {
        private readonly IUOW _uow;

        // for Web API ninject integration!!!!
        // install-package Ninject.Web.WebApi
        // install-package Ninject.Web.WebApi.WebHost

        public ValuesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET api/values
        public string Get()
        {
            var userName = User.Identity.GetUserName();
            return $"Hello, {userName}. DT is "+DateTime.Now + " Persons in DB:"+_uow.Persons.All.Count;
        }
    }
}