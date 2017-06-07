using System;
using System.Collections.Generic;
using System.Linq;

namespace RefreshingCache
{
    public class CacheService : ICacheService
    {
        private Dictionary<string, CacheItem> _cache;
        private IFetchValues _externalService;
        
        public CacheService(IFetchValues externalService)
        {
            _cache = new Dictionary<string, CacheItem>();
            _externalService = externalService;
        }

        public const int MaxSize = 3;

        public CacheItem Get(string key)
        {
            if(_cache.Count == MaxSize)
            {
                var leastRecentKey = GetLeastRecentlyUsed();
                _cache.Remove(leastRecentKey);
            }

            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, new CacheItem(_externalService.Fetch(key)));
            }

            _cache[key].Updated = DateTime.Now;
            return _cache[key];
        }

        public List<CacheItem> GetAllValues()
        {
            return _cache.Values.ToList();
        }

        public void Load(Dictionary<string, CacheItem> cache)
        {
            _cache = cache;
        }
        
        private string GetLeastRecentlyUsed()
        {
            DateTime leastRecentDate = DateTime.Now;
            string leastRecent = null;
            foreach (var key in _cache.Keys)
            {
                if (_cache[key].Updated < leastRecentDate)
                {
                    leastRecentDate = _cache[key].Updated.Value;
                    leastRecent = key;
                }
            }

            return leastRecent;
        }
    }
}
