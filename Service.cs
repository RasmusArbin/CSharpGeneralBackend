﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGeneral.Interfaces;
using BackendGeneral.Providers;

namespace BackendGeneral
{
    public abstract class Service<T, T2, T3, T4> 
        where T : class
        where T2 : Repository<T>
        where T3 : ServiceProvider
        where T4 : ICache, new()
    {
        protected readonly T2 MainRepository;
        public readonly T3 ServiceProvider;
        private readonly T4 _cache;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        //TODO: Web.config
        private const string Dependency = "dependency";
        private const string Expression = "expression";
        private const string Item = "item";

        protected Service(T2 repository, T3 serviceProvider, IDbContext dbContext, ILogger logger)
        {
            MainRepository = repository;
            ServiceProvider = serviceProvider;
            _cache = new T4();
            _dbContext = dbContext;
            _logger = logger;
        }

        public virtual T GetById(int id)
        {
            return MainRepository.GetById(id);
        }

        public virtual void Insert(T entity)
        {
            MainRepository.Insert(entity);

            //Dependencies
            string objectName = typeof(T).Name;
            RemoveCacheDependencyExpressions(objectName);

            //Logging
            _logger.LogInsert(entity);
        }

        public virtual void Delete(int id)
        {
            //Get the item so that we can logg it
            T entity = MainRepository.GetById(id);

            MainRepository.Delete(id);

            //Logging
            _logger.LogDelete(entity);
        }

        public virtual void Update(T entity)
        {
            MainRepository.Update(entity);

            //Dependencies
            string objectName = typeof(T).Name;
            RemoveCacheDependencyExpressions(objectName);

            //Logging
            _logger.LogUpdate(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return MainRepository.GetAll();
        }

        protected void CacheQuery<T6>(string expression, List<T6> items)
        {
            //Is an type that can be identified by an Id
            if (typeof(T6).GetInterfaces().Contains(typeof(IIdentifiable)))
            {
                List<IIdentifiable> lstIdentifiables = items.Cast<IIdentifiable>().ToList();

                string objectName = typeof(T6).Name;
                foreach (IIdentifiable item in lstIdentifiables)
                {
                    _cache.Set(string.Format("{0}_{1}_{2}", Item, objectName, item.Id), item);
                }

                _cache.Set(string.Format("{0}_{1}", Expression, expression), lstIdentifiables.Select(item => item.Id).ToList());
            }
            else
            {
                _cache.Set(string.Format("{0}_{1}", Expression, expression), items);
            }
        }

        public List<T6> ReadQuery<T6>(IQueryable<T6> query)
        {
            string expression = _dbContext.GetQueryableAsString<T6>(query);

            List<int> lstIds = _cache.Get<List<int>>(string.Format("{0}_{1}", Expression, expression));

            List<T6> lstCacheItems;
            if (lstIds != null)
            {
                lstCacheItems = new List<T6>();
                string objectName = typeof(T6).Name;
                foreach (int id in lstIds)
                {
                    lstCacheItems.Add(_cache.Get<T6>(string.Format("{0}_{1}_{2}", Item, objectName, id)));
                }
            }
            else
            {
                lstCacheItems = query
                    .ToList();

                CacheQuery(expression, lstCacheItems);

                //Dependencies
                List<string> dependencies = _dbContext.GetDependencies(query);
                foreach (string dependency in dependencies)
                {
                    CacheDependency(expression, dependency);
                }
            }
            return lstCacheItems;
        }

        private void CacheDependency(string expression, string dependency)
        {
            List<string> expressions = _cache.Get<List<string>>(string.Format("{0}_{1}", Dependency, dependency));
            expressions = expressions ?? new List<string>();
            expressions.Add(expression);

            _cache.Set(string.Format("{0}_{1}", Dependency, dependency), expressions);
        }

        private void RemoveCacheDependencyExpressions(string dependency)
        {
            List<string> expressions = _cache.Get<List<string>>(string.Format("{0}_{1}", Dependency, dependency));

            if (expressions != null)
            {
                foreach (string expression in expressions)
                {
                    _cache.Remove(string.Format("{0}_{1}", Expression, expression));
                }

                _cache.Set(string.Format("{0}_{1}", Dependency, dependency), new List<string>());
            }
        }
    }
}
