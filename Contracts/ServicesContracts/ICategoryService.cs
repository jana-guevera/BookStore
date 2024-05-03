using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesContracts
{
    public interface ICategoryService
    {
        /// <summary>
        /// Add the category to the categories table and returns the added category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Category</returns>
        Task<Category> AddAsync(Category category);

        /// <summary>
        /// Update the category from category table and returns the updated category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Category</returns>
        Task<Category> UpdateAsync(Category category);

        /// <summary>
        /// Remove the category from the category table and returns the removed category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category</returns>
        Task<Category> RemoveAsync(int id);

        /// <summary>
        /// Find and returns a single category based on the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category</returns>
        Task<Category> FindByIdAsync(int id);

        /// <summary>
        /// Returns all the categories from the categories table
        /// </summary>
        /// <returns>IEnumerable<Category></returns>
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
