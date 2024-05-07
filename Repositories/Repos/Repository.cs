using Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BookStoreDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();    
        }

        public async Task<T> AddAsync(T record)
        {
            _dbSet.Add(record);
            await _dbContext.SaveChangesAsync();  
            return record;
        }

		public async Task<T> UpdateAsync(T record)
		{
			_dbSet.Update(record);
			await _dbContext.SaveChangesAsync();
			return record;
		}

		public async Task<T> RemoveAsync(T record)
		{
			_dbSet.Attach(record);
			_dbSet.Remove(record);
			await _dbContext.SaveChangesAsync();
			return record;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
	}
}
