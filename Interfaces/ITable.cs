using System.Collections.Generic;
using System.Linq;

namespace BackendGeneral.Interfaces
{
    public interface ITable<T>
        where T : class
    {
        IQueryable<T> GetAll();
        void Remove(T entity);
        void Insert(T entity);
        void Update(T entity);
        T GetById(int id);
    }
}