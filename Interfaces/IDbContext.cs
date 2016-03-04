using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendGeneral.Interfaces
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangedAsync();
        
        ITable<TEntity> DbSet<TEntity>()
            where TEntity : class;

        string GetQueryableAsString<T>(IQueryable<T> queryable);

        List<string> GetDependencies<T>(IQueryable<T> queryable);
    }
}
