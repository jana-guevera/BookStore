using Contracts.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repositories.Caching;
using Repositories.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BookStoreDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        protected readonly CustomMemoryCache _memoryCache;
        protected readonly string _parentCacheKey;

        public Repository(BookStoreDbContext dbContext, CustomMemoryCache memoryCache, string parentCacheKey)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();    
            _memoryCache = memoryCache;
            _parentCacheKey = parentCacheKey;
        }

        public async Task<T> AddAsync(T record)
        {
            _dbSet.Add(record);
            await _dbContext.SaveChangesAsync();

            _memoryCache.ClearCache(_parentCacheKey);
            return record;
        }

		public async Task<T> UpdateAsync(T record)
		{
			_dbSet.Update(record);
			await _dbContext.SaveChangesAsync();

            _memoryCache.ClearCache(_parentCacheKey);
            return record;
		}

		public async Task<T> RemoveAsync(T record)
		{
			_dbSet.Attach(record);
			_dbSet.Remove(record);
			await _dbContext.SaveChangesAsync();

            _memoryCache.ClearCache(_parentCacheKey);
			return record;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
        {
            var records = _memoryCache.GetCache<IEnumerable<T>>(_parentCacheKey, "");

            if (records == null)
            {
                records = await _dbSet.ToListAsync();
                _memoryCache.SetCache<IEnumerable<T>>(_parentCacheKey, "", records);
            }

            return records;
        }
	}
}
