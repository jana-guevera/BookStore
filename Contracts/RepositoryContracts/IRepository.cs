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
        /// Find and returns single record based on the provided expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Returns all the records from the table
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Add the record to the table
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> Add(T item);

        /// <summary>
        /// Update the record from the table
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> Update(T item);
    }
}
