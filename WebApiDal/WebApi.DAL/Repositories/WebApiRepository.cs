using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using Interfaces.Repository;
using NLog;

namespace WebApi.DAL.Repositories
{
    public class WebApiRepository<T> : IRepository<T> where T : class
    {
        protected HttpClient HttpClient;
        protected string EndPoint;

        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public WebApiRepository(HttpClient httpClient, string endPoint)
        {
            HttpClient = httpClient;
            EndPoint = endPoint;
        }

        public List<T> All
        {
            get
            {
                var response = HttpClient.GetAsync(EndPoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsAsync<List<T>>().Result;
                    return res;
                }
                _logger.Debug("Web API statuscode: " + response.StatusCode.ToString() + " Uri:" + response.RequestMessage.RequestUri);
                return new List<T>();
            }
        }

        public List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public T GetById(params object[] id)
        {
            var query = id[0];

            for (int i = 1; i < id.Length; i++)
            {
                query = query + "/"+id;
            }

            var response = HttpClient.GetAsync(EndPoint+query).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<T>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode +"("+(int)response.StatusCode+")" + " Uri:" + response.RequestMessage.RequestUri);
            return null;

        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(params object[] id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
