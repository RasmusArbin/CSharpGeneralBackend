using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendGeneral.Interfaces
{
    public interface ITable<T>
        where T : class
    {
        IQueryable<T> GetAll();
        Task<IQueryable<T>> GetAllAsync();
        
        void Remove(T entity);
        void Insert(T entity);
        void Update(T entity);
        
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
    }
}
