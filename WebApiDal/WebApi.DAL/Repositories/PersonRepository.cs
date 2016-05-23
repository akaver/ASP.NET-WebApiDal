using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repositories;
using Microsoft.Owin.Security;

namespace WebApi.DAL.Repositories
{
    public class PersonRepository : WebApiRepository<Person>, IPersonRepository
    {
        public PersonRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<Person> GetAllForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Person GetForUser(int personId, int userId)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAllForUser(int userId, string filter, string sortProperty, int pageNumber, int pageSize, out int totalUserCount,
            out string realSortProperty)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAllForUser(int userId, string filter, DateTime? filterFromDT, DateTime? filterToDt, string sortProperty,
            int pageNumber, int pageSize, out int totalUserCount, out string realSortProperty)
        {
            throw new NotImplementedException();
        }
    }
}
