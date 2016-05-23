using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Interfaces.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NLog;
using WebApi.DAL.Helpers;

namespace WebApi.DAL.Repositories
{
    public class WebApiRepository<T> : IBaseRepository<T> where T : class
    {
        protected HttpClient HttpClient;
        protected string EndPoint;
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IAuthenticationManager _authenticationManager;


        public WebApiRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient), typeof(T).FullName);
            }
            HttpClient = httpClient;

            if (endPoint == null)
            {
                throw new ArgumentNullException(nameof(endPoint), "Web API baserepo repo for type:" + typeof(T).FullName);
            }
            EndPoint = endPoint;
            _authenticationManager = authenticationManager;
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
                _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    //redirect client to login page somehow
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.Redirect(@"~/Account/Login");   
                }
                return new List<T>();
            }
        }

        public List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException("Not possible in Web API!?!?");
        }

        public T GetById(params object[] id)
        {
            var uri = id[0];
            for (int i = 1; i < id.Length; i++)
            {
                uri = uri + "/" + id[i];
            }

            var response = HttpClient.GetAsync(EndPoint + uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<T>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return null;

        }

        public void Add(T entity)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint, entity).Result;
            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            // TODO: update entity key?
        }

        public void Update(T entity)
        {
            var keys = GetEntityKeys(entity).OrderBy(k => k.Order).ToArray();
            if (keys == null || keys.Length == 0)
            {
                throw new KeyNotFoundException("Primary key(s) not detected in entity type: " + typeof(T).FullName);
            }

            var uri = keys[0].Value.ToString();
            for (int i = 1; i < keys.Length; i++)
            {
                uri = uri + "/" + keys[i].Value;
            }

            uri = EndPoint + uri;
            var response = HttpClient.PutAsJsonAsync(uri, entity).Result;
            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }

        public void Delete(T entity)
        {
            var keys = GetEntityKeys(entity).OrderBy(k => k.Order).ToArray();
            if (keys == null || keys.Length == 0)
            {
                throw new KeyNotFoundException("Primary key(s) not detected in entity type: " + typeof(T).FullName);
            }

            var uri = keys[0].Value.ToString();
            for (int i = 1; i < keys.Length; i++)
            {
                uri = uri + "/" + keys[i].Value;
            }

            uri = EndPoint + uri;

            var response = HttpClient.DeleteAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }

        public void Delete(params object[] id)
        {
            // endpoint/id0/id1/id2/...
            var uri = id[0].ToString();
            for (int i = 1; i < id.Length; i++)
            {
                uri = uri + "/" + id[i];
            }

            var response = HttpClient.DeleteAsync(EndPoint + uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }

        public void Dispose()
        {
        }


        public List<EntityKeyInfo> GetEntityKeys(T entity)
        {
            var res = new List<EntityKeyInfo>();

            var className = typeof(T).Name.ToLower();
            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                var columOrder = 0;
                var isKey = false;

                // lets check the [Key] and [Column(Order=xx)] attributes
                object[] attrs = propertyInfo.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr is KeyAttribute)
                    {
                        isKey = true;
                    }

                    var attribute = attr as ColumnAttribute;
                    if (attribute != null)
                    {
                        columOrder = attribute.Order;
                    }
                }

                if (isKey)
                {
                    res.Add(new EntityKeyInfo(propertyInfo.Name, propertyInfo.GetValue(entity, null), columOrder));
                }
            }

            // if key(s) are already found, return
            if (res.Count > 0) return res;

            // no keys yet, check for property name
            foreach (var propertyInfo in properties)
            {
                var name = propertyInfo.Name.ToLower();
                if (name.Equals(className + "id") || name.Equals("id"))
                {
                    res.Add(new EntityKeyInfo() { PropertyName = propertyInfo.Name, Value = propertyInfo.GetValue(entity, null) });
                }

            }

            return res;
        }
    }
}
