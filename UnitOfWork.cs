using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using BackendGeneral.Interfaces;
using BackendGeneral.Providers;

namespace BackendGeneral
{
    public abstract class UnitOfWork<T, T2> : IDisposable
        where T : IDbContext, new() 
        where T2: ServiceProvider
    {
        private T _dbContext;
        protected T DbContext
        {
            get
            {
                return Equals(_dbContext, default(T))
                    ? (_dbContext = new T())
                    : _dbContext;
            }
        }

        public void ExcecuteStatement(Action<T, T2> statement)
        {
            statement(DbContext, ServiceProvider);
        }

        public T3 ReadStatement<T3>(Func<T, T2, T3> statement)
        {
            return statement(DbContext, ServiceProvider);
        }

        protected abstract T2 ServiceProvider { get; }
        public void Dispose()
        {
            if(_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
