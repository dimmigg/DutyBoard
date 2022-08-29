using System;
using System.Collections.Generic;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> filter = null);

        T FirstOrDefault(Func<T, bool> filter = null);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        void Upsert(T entity);
    }
}
