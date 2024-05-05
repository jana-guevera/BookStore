﻿using Contracts.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> FindByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}