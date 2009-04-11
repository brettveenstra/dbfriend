// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IMsSqlDependencyRepository.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IMsSqlDependencyRepository type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    using System.Collections.Generic;

    /// <summary>
    /// </summary>
    public interface IMsSqlDependencyRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="mssqlObject">
        /// The ms sql object.
        /// </param>
        /// <returns>
        /// </returns>
        IEnumerable<IMsSqlObject> GetDependencies(IMsSqlObject mssqlObject);
    }
}