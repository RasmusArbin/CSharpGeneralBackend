using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using BackendGeneral.Interfaces;

namespace BackendGeneral
{
    public abstract class Repository<T> : IDisposable
        where T : class, IIdentifiable 
    {

        protected IDbContext DBContext { get; private set; }
        protected ITable<T> DBSet;

        public Repository()
        {

        }

        protected Repository(IDbContext dbContext)
        {
            Bind(dbContext);
        }

        public void Bind(IDbContext dbContext)
        {
            DBSet = dbContext.DbSet<T>();
            DBContext = dbContext;
        }

        public T GetById(int id)
        {
            return DBSet.GetById(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DBSet.GetByIdAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return DBSet.GetAll();
        }

        public virtual void Insert(T entity)
        {
            DBSet.Insert(entity);

            DBContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            DBSet.Remove(entity);

            DBContext.SaveChanges();
        }

        /// <summary>
        /// TODO: Remove?
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            DBSet.Update(entity);
            DBContext.SaveChanges();
        }

        public virtual async Task InsertAsync(T entity)
        {
            DBSet.Insert(entity);

            await DBContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = GetById(id);
            DBSet.Remove(entity);

            await DBContext.SaveChangesAsync();
        }

        /// <summary>
        /// TODO: Remove?
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task UpdateAsync(T entity)
        {
            DBSet.Update(entity);
            await DBContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }
    }
}
