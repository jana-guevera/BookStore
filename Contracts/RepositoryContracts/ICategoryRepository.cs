using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Find a category by its name or return null
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Category or Null</returns>
        Task<Category> FindByName(string name);
    }
}
