using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces.Repositories
{
    // this is the base repository interface for all EF repositories
    public interface IBaseRepository<T> : IDisposable
        where T : class
    {
        // gett all records in table
        //IQueryable<T> All { get; }
        List<T> All { get; }

        // get all records with filter
        //IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T GetById(params object[] id);
        void Add(T entity);
        void Update(T entity);
        //void UpdateOrInsert(T entity);
        void Delete(T entity);
        void Delete(params object[] id);
    }
}