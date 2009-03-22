// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="StoredProcedureIDbScriptObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the StoredProcedureIDbScriptObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class StoredProcedureIDbScriptObjectMapper : IStoredProcedureMsSqlObjectMapper
    {
        #region IStoredProcedureMsSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(StoredProcedure from)
        {
            return new MsSqlStoredProc(from);
        }

        #endregion
    }
}