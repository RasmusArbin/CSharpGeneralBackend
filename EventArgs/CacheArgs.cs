using System.Collections.Generic;

namespace BackendGeneral.EventArgs
{
    public class CacheArgs<T>
    {
        public List<T> CacheItems { get; private set; }

        public CacheArgs(T cacheItems)
        {
            CacheItems = new List<T> {cacheItems};
        }

        public CacheArgs(List<T> cacheItemses)
        {
            CacheItems = cacheItemses;
        }
    }
}
