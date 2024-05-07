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
		/// Find and returns single category based on the provided id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Category</returns>
		Task<Category> FindOneByIdAsync(int id);

		/// <summary>
		/// Find and returns single Category based on the provided name
		/// </summary>
		/// <param name="name"></param>
		/// <returns>Category</returns>
		Task<Category> FindOneByNameAsync(string name);
	}
}
