using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using NLog;

namespace DAL.Helpers
{
    public class EFRepositoryProvider : IEFRepositoryProvider, IDisposable
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        public IDbContext DbContext { get; set; }
        private readonly EFRepositoryFactories _repositoryFactories;
        protected Dictionary<Type, object> Repositories { get; private set; }

        public EFRepositoryProvider(EFRepositoryFactories repositoryFactories, ILogger logger)
        {
            _repositoryFactories = repositoryFactories;
            _logger = logger;

            _logger.Debug("InstanceId: " + _instanceId);

            Repositories = new Dictionary<Type, object>();
        }

        public IEFRepository<T> GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IEFRepository<T>>(_repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        public virtual T GetRepository<T>(Func<IDbContext, object> factory = null) where T : class
        {
            // Look for T dictionary cache under typeof(T).
            object repoObj;
            Repositories.TryGetValue(typeof (T), out repoObj);
            if (repoObj != null)
            {
                return (T) repoObj;
            }

            // Not found or null; make one, add to dictionary cache, and return it.
            return MakeRepository<T>(factory, DbContext);
        }

        public void SetRepository<T>(T repository)
        {
        }

        protected virtual T MakeRepository<T>(Func<IDbContext, object> factory, IDbContext dbContext)
        {
            var f = factory ?? _repositoryFactories.GetRepositoryFactory<T>();
            if (f == null)
            {
                throw new NotImplementedException("No factory for repository type, " + typeof (T).FullName);
            }
            var repo = (T) f(dbContext);
            Repositories[typeof (T)] = repo;
            return repo;
        }

        public void Dispose()
        {
            _logger.Debug("InstanceId: " + _instanceId);
        }
    }
}