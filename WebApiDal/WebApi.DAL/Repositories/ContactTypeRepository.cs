using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repository;

namespace WebApi.DAL.Repositories
{
    public class ContactTypeRepository : WebApiRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }
}
