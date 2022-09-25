using System.Collections.Generic;

namespace DutyBoard_DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        void ClearTable(string table);
        void Upsert(T entity);
        void InsertData(IEnumerable<T> data);
        IEnumerable<T> GetAll(int? id = null);
        T FirstOrDefault(int? id = null);
    }
}
