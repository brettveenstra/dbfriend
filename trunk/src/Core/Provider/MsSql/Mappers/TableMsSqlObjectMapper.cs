// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TableMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the TableMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class TableMsSqlObjectMapper : ITableMsSqlObjectMapper
    {
        #region ITableMsSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(Table from)
        {
            return new MsSqlTable(from);
        }

        #endregion
    }
}