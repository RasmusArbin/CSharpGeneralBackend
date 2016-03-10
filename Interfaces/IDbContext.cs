using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendGeneral.Interfaces
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        
        ITable<TEntity> DbSet<TEntity>()
            where TEntity : class;

        string GetQueryableAsString<T>(IQueryable<T> queryable);

        List<string> GetDependencies<T>(IQueryable<T> queryable);

        List<T> ReadQuery<T>(IQueryable<T> query);

        Task<List<T>> ReadQueryAsync<T>(IQueryable<T> query);
    }
}
