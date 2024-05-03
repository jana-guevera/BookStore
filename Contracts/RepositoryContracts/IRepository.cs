using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Add the <typeparamref name="T"/> to the table and returns the <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> AddAsync(T record);

        /// <summary>
        /// Update the <typeparamref name="T"/> from the table and returns the <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> UpdateAsync(T record);

        /// <summary>
        /// Removes a <typeparamref name="T"/> from the table and returns the <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> RemoveAsync(T record);

        /// <summary>
        /// Find and returns single <typeparamref name="T"/> based on the provided expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Find and returns multiple <typeparamref name="T"/> based on the provided expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Returns all the <typeparamref name="T"/> from the table
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
    }

   
}
