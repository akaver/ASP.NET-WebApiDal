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
    public class TranslationRepository : WebApiRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }
}
