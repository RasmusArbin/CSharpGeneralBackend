using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using BackendGeneral.Interfaces;

namespace BackendGeneral
{
    public abstract class Repository<T> : IDisposable
        where T : class
    {

        protected IDbContext DBContext { get; private set; }
        protected readonly ITable<T> DBSet;

        protected Repository(IDbContext dbContext)
        {
            DBSet = dbContext.DbSet<T>();
            DBContext = dbContext;
        }

        public abstract T GetById(int id);

        public virtual IQueryable<T> GetAll()
        {
            return DBSet.GetAll();
        }

        public virtual void Insert(T entity)
        {
            DBSet.Insert(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            DBSet.Remove(entity);
        }

        /// <summary>
        /// TODO: Remove?
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            DBSet.Update(entity);
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
