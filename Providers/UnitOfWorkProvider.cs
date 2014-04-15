using BackendGeneral.Interfaces;

namespace BackendGeneral.Providers
{
    public abstract class UnitOfWorkProvider<T, T2, T3>
        where T : UnitOfWork<T2, T3>, new()
        where T2 : IDbContext, new()
        where T3 : ServiceProvider
    {
        protected IDbContext DbContext;
        protected T2 ServiceProvider;

        protected UnitOfWorkProvider()
        {
            DbContext = new T2();
        }

        private T _unitOfWork; 
        public T UnitOfWork
        {
            get
            {
                return _unitOfWork ?? (_unitOfWork = new T());
            }
        }
    }
}
