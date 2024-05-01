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
        /// Add the record to the table and returns the record
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> Add(T item);

        /// <summary>
        /// Update the record from the table and returns the record
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> Update(T item);

        /// <summary>
        /// Removes a record from the table and returns the record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Remove(int id);

        /// <summary>
        /// Find and returns single record based on the provided expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> FindOne(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Find and returns multiple records based on the provided expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindMany(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Returns all the records from the table
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();
    }
}
