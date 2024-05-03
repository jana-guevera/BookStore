using Contracts.RepositoryContracts;
using Domain.Entities;
using Repositories.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repos
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
