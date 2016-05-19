using System.Net.Http;
using Domain;
using Interfaces.Repository;

namespace WebApi.DAL.Repositories
{
    public class MultiLangStringRepository : WebApiRepository<MultiLangString>, IMultiLangStringRepository
    {
        public MultiLangStringRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }
    }
}
