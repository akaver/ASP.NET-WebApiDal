using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using DAL.Interfaces;
using Interfaces.Repository;
using Interfaces.UOW;
using WebApi.DAL.Repositories;

namespace WebApi.DAL
{
    public class WebApiUOW : IUOW, IDisposable
    {
        private readonly IDictionary<Type, Func<HttpClient, object>> _repositoryFactories;
        protected Dictionary<Type, object> Repositories { get; private set; }

        private readonly HttpClient _httpClient = new HttpClient();

        public WebApiUOW()
        {
            // get the base Uri from config
            var baseUri = ConfigurationManager.AppSettings["WebApi_BaseAddress"];
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new Exception("WebApi_BaseAddress not set in config!");
            }
            _httpClient.BaseAddress = new Uri(baseUri);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //list of factories for our repos
            _repositoryFactories = GetCustomFactories();

            // cache for already create repos
            Repositories = new Dictionary<Type, object>();
        }

        public IContactTypeRepository ContactTypes => GetWebApiRepo<IContactTypeRepository>();
        public IMultiLangStringRepository MultiLangStrings => GetWebApiRepo<IMultiLangStringRepository>();
        public ITranslationRepository Translations => GetWebApiRepo<ITranslationRepository>();
        public IPersonRepository Persons => GetWebApiRepo<IPersonRepository>();
        public IContactRepository Contacts => GetWebApiRepo<IContactRepository>();
        public IArticleRepository Articles => GetWebApiRepo<IArticleRepository>();
        public IUserIntRepository UsersInt => GetWebApiRepo<IUserIntRepository>();
        public IUserRoleIntRepository UserRolesInt => GetWebApiRepo<IUserRoleIntRepository>();
        public IRoleIntRepository RolesInt => GetWebApiRepo<IRoleIntRepository>();
        public IUserClaimIntRepository UserClaimsInt => GetWebApiRepo<IUserClaimIntRepository>();
        public IUserLoginIntRepository UserLoginsInt => GetWebApiRepo<IUserLoginIntRepository>();

        private T GetWebApiRepo<T>() where T : class
        {

            // Look for T dictionary cache under typeof(T).
            object repoObj;
            Repositories.TryGetValue(typeof(T), out repoObj);
            if (repoObj != null)
            {
                return (T)repoObj;
            }

            return MakeRepository<T>();
        }

        protected virtual T MakeRepository<T>()
        {
            Func<HttpClient, object> factory;

            _repositoryFactories.TryGetValue(typeof(T), out factory);
            if (factory == null)
            {
                throw new NotImplementedException("No factory for repository type: " + typeof(T).FullName);
            }
            var repo = (T)factory(_httpClient);
            Repositories[typeof(T)] = repo;
            return repo;
        }

        private static IDictionary<Type, Func<HttpClient, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<HttpClient, object>>
                {
                    {typeof(IContactTypeRepository), (httpClient) => new ContactTypeRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_ContactTypes"])},
                    {typeof(IMultiLangStringRepository), (httpClient) => new MultiLangStringRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_MultiLangStrings"])},
                    {typeof(ITranslationRepository), (httpClient) => new TranslationRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_Translations"])},
                    {typeof(IPersonRepository), (httpClient) => new PersonRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_Persons"])},
                    {typeof(IContactRepository), (httpClient) => new ContactRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_Contacts"])},
                    {typeof(IArticleRepository), (httpClient) => new ArticleRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"])},
                    {typeof(IUserIntRepository), (httpClient) => new UserIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UsersInt"])},
                    {typeof(IUserRoleIntRepository), (httpClient) => new UserRoleIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserRolesInt"])},
                    {typeof(IRoleIntRepository), (httpClient) => new RoleIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_RolesInt"])},
                    {typeof(IUserClaimIntRepository), (httpClient) => new UserClaimIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserClaimsInt"])},
                    {typeof(IUserLoginIntRepository), (httpClient) => new UserLoginIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserLoginsInt"])},

                };
        }

        /// <summary>
        /// Dummy method in Web API UOW
        /// </summary>
        public void Commit()
        {
        }

        public T GetRepository<T>() where T : class
        {
            var res = GetWebApiRepo<T>() as T;
            if (res == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(T).FullName);
            }
            return res;
        }


        public void Dispose()
        {
            _httpClient.Dispose();
        }


    }
}
