using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.Caching
{
    public class CustomMemoryCache : MemoryCache
    {
        Dictionary<string, List<string>> keys = new Dictionary<string, List<string>>();

        public CustomMemoryCache(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        public CustomMemoryCache() : base(new MemoryCacheOptions
        {
            ExpirationScanFrequency = TimeSpan.FromMinutes(1),
        }){}

        public T GetCache<T>(string parentKey, string key) where T : class
        {
            string cacheKey = parentKey + key;

            return this.Get<T>(cacheKey);
        }    

        public void SetCache<T>(string parentKey, string key, T value) where T : class
        {
            string cacheKey = parentKey + key;

            var cachedEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(180));

            this.Set(cacheKey, value, cachedEntryOptions);

            if (keys.ContainsKey(parentKey))
            {
                List<string> subkeys = keys[parentKey];
                if (!subkeys.Contains(cacheKey))
                {
                    subkeys.Add(cacheKey);
                }
            }
            else
            {
                List<string> subkeys = new List<string>();
                subkeys.Add(cacheKey);
                keys.Add(parentKey, subkeys);
            }
        }

        public void ClearCache(string parentKey)
        {
            if (keys.ContainsKey(parentKey))
            {
                List<string> subKeys = keys[parentKey];

                if (subKeys == null) return;

                foreach (string key in subKeys)
                {
                    this.Remove(key);
                }

                keys[parentKey] = new List<string>();
            }
        }
    }
}
