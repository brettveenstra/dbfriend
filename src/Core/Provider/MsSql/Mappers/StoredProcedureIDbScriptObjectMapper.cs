// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="StoredProcedureIDbScriptObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the StoredProcedureIDbScriptObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class StoredProcedureIDbScriptObjectMapper : IStoredProcedureAdapterMsSqlObjectMapper
    {
        private IMsSqlStatementsTransformer transformer;

        #region IStoredProcedureAdapterMsSqlObjectMapper Members

        public StoredProcedureIDbScriptObjectMapper(IMsSqlStatementsTransformer transformer)
        {
            this.transformer = transformer;
        }

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(IStoredProcedureAdapter from)
        {
            return new MsSqlStoredProc(from, transformer);
        }

        #endregion
    }
}