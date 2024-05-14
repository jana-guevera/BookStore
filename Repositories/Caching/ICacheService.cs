using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Caching
{
    public interface ICacheService
    {
        Task SetAsync<T>(string parentKey, string key, T data, TimeSpan? absoluteTime = null);

        Task<T> GetAsync<T>(string parentKey, string key);

        Task RemoveAsync(string parentKey, string key);

        Task ClearParentAsync(string parentKey);
    }
}
