// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IRepository.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IRepository type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace DbFriend.Core
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerable<T> FindAll();
    }
}