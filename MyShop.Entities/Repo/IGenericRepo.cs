using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyShop.Entities.Repo
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includes);

        T GetById(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
