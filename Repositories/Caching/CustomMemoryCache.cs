using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Caching
{
    public class CustomMemoryCache : ICacheService
    {
        Dictionary<string, List<string>> keys = new Dictionary<string, List<string>>();
        MemoryCache _cache;

        public CustomMemoryCache() 
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromSeconds(10),
            });
        }

        public Task SetAsync<T>(string parentKey, string key, T data, TimeSpan? absoluteTime = null)
        {
            string cacheKey = parentKey + key;

            TimeSpan absoluteExpiry = absoluteTime ?? TimeSpan.FromMinutes(5);

            var cachedEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(absoluteExpiry);

            _cache.Set(cacheKey, data, cachedEntryOptions);

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

            return Task.CompletedTask;
        }

        public Task<T> GetAsync<T>(string parentKey, string key)
        {
            string cacheKey = parentKey + key;

            var data = _cache.Get<T>(cacheKey);
            return Task.FromResult(data);
        }

        public Task RemoveAsync(string parentKey, string key)
        {
            throw new NotImplementedException();
        }

        public Task ClearParentAsync(string parentKey)
        {
            if (keys.ContainsKey(parentKey))
            {
                List<string> subKeys = keys[parentKey];

                if (subKeys == null) return Task.CompletedTask;

                foreach (string key in subKeys)
                {
                    _cache.Remove(key);
                }

                keys[parentKey] = new List<string>();
            }

            return Task.CompletedTask;
        }
    }
}
