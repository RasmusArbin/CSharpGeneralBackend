using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        public virtual IQueryable<T> GetAll()
        {
            return DBSet.GetAll();
        }

        public virtual void Insert(T entity)
        {
            DBSet.Insert(entity);

            SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            DBSet.Remove(entity);

            SaveChanges();
        }

        /// <summary>
        /// TODO: Remove?
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            DBSet.Update(entity);
            SaveChanges();
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }

        /// <summary>
        /// User for special cases
        /// DO NOT OVERUSE
        /// </summary>
        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }
    }
}
