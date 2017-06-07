using System;

namespace RefreshingCache
{
    public class CacheItem
    {
        public CacheItem(string value, DateTime? created = null)
        {
            Value = value;
            Created = created ?? DateTime.Now;
        }

        public DateTime? Updated { get; set; }
        
        public string Value { get; }

        public DateTime Created { get; }

        public bool IsExpired
        {
            get
            {
                return DateTime.Now.AddMinutes(-60) < Created;
            }
        }
    }
}
