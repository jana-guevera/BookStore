using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repositories.Caching
{
    public class RedisCache : ICacheService
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabase _cache;
        private ConnectionMultiplexer _multiplexer;

        Dictionary<string, List<string>> keys = new Dictionary<string, List<string>>();

        public RedisCache(IConfiguration configuration)
        
        {
            ConfigurationOptions options = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
            options.AllowAdmin = true;

            _multiplexer = ConnectionMultiplexer.Connect(options);
            _cache = _multiplexer.GetDatabase();

            var endpoints = _multiplexer.GetEndPoints();
            var server = _multiplexer.GetServer(endpoints[0]);
            server.FlushAllDatabases();
        }

        public async Task SetAsync<T>(string parentKey, string key, T data, TimeSpan? absoluteTime = null)
        {
            string cacheKey = parentKey + key;

            TimeSpan expiry = absoluteTime ?? TimeSpan.FromMinutes(5);

            string jsonData = JsonSerializer.Serialize(data);

            await _cache.StringSetAsync(cacheKey, jsonData, expiry);

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

        public async Task<T> GetAsync<T>(string parentKey, string key)
        {
            var jsonData = await _cache.StringGetAsync(parentKey + key);

            if (jsonData.IsNull)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public async Task RemoveAsync(string parentKey, string key)
        {
            await _cache.KeyDeleteAsync(parentKey + key);

            if (keys.ContainsKey(parentKey))
            {
                keys[parentKey].Remove(parentKey + key);
            }
        }

        public async Task ClearParentAsync(string parentKey)
        {
            if (!keys.ContainsKey(parentKey)) return;

            foreach(string key in keys[parentKey])
            {
                await _cache.KeyDeleteAsync(key);
            }

            keys[parentKey] = new List<string>();
        }

    }
}
