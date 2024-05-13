using Contracts.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repositories.Caching;
using Repositories.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
		private static readonly string parenCachetKey = "Category_";
        public CategoryRepository(BookStoreDbContext dbContext, CustomMemoryCache memoryCache) 
			: base(dbContext, memoryCache, parenCachetKey)
        {
        }

		public async Task<Category> FindOneByIdAsync(int id)
		{
			var category = _memoryCache.GetCache<Category>(parenCachetKey, id.ToString());

			if (category == null)
			{
				category = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
				if(category != null)
				{
					_memoryCache.SetCache<Category>(_parentCacheKey, id.ToString(), category);
				}
            }

			return category;
		}

		public async Task<Category> FindOneByNameAsync(string name)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
		}
	}
}
