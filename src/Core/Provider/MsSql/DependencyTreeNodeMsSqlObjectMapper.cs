// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DependencyTreeNodeMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DependencyTreeNodeMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    using System;

    using DbFriend.Core.Provider.MsSql.Adapters;

    /// <summary>
    /// </summary>
    public class DependencyTreeNodeMsSqlObjectMapper : IDependencyTreeNodeAdapterMsSqlObjectMapper
    {
        #region IDependencyTreeNodeAdapterMsSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IMsSqlObject MapFrom(IDependencyTreeNodeAdapter from)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}