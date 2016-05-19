using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain;
using Interfaces.Repository;

namespace WebApi.DAL.Repositories
{
    public class PersonRepository : WebApiRepository<Person>, IPersonRepository
    {
        public PersonRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
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
