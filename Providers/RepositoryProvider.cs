using BackendGeneral.Interfaces;
using System.Collections.Generic;
using System;

namespace BackendGeneral.Providers
{
    public class RepositoryProvider
    {
        public IDbContext DbContext;
        protected RepositoryProvider(IDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
