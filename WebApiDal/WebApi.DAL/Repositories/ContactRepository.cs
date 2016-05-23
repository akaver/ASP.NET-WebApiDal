using System.Collections.Generic;
using System.Net.Http;
using Domain;
using Interfaces.Repositories;
using Microsoft.Owin.Security;

namespace WebApi.DAL.Repositories
{
    public class ContactRepository : WebApiRepository<Contact>, IContactRepository
    {
        public ContactRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
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