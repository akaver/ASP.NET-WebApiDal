using System.Net.Http;
using Domain;
using Interfaces.Repository;

namespace WebApi.DAL.Repositories
{
    public class TranslationRepository : WebApiRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }
}
