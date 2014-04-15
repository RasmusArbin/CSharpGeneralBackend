using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Collections.Generic;

namespace BackendGeneral.Interfaces
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        ITable<TEntity> DbSet<TEntity>()
            where TEntity : class;

        string GetQueryableAsString<T>(IQueryable<T> queryable);

        List<string> GetDependencies<T>(IQueryable<T> queryable);
    }
}
