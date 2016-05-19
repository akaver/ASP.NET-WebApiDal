using System.Collections.Generic;
using System.Net.Http;
using DAL.Interfaces;
using Domain;

namespace WebApi.DAL.Repositories
{
    public class ContactRepository: WebApiRepository<Contact>, IContactRepository
    {
        public ContactRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }

        public List<Contact> AllforUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Contact GetForUser(int contactId, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
