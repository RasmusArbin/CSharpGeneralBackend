using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGeneral.Interfaces;

namespace BackendGeneral.Providers
{
    public abstract class ServiceProvider
    {
        protected IDbContext DbContext;
        protected ServiceProvider(IDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
