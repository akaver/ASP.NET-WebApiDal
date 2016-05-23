using System;
using System.Web.Http;
using DAL.Interfaces;
using Interfaces.UOW;

namespace WebApi.Server.Controllers.Api
{
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
            return DateTime.Now + " " + _uow.ContactTypes.All.Count;
        }
    }
}
