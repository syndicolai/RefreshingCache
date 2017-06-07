using System.Collections.Generic;

namespace RefreshingCache
{
    interface ICacheService
    {
        CacheItem Get(string key);
        
        void Load(Dictionary<string, CacheItem> cache);
    }
}
