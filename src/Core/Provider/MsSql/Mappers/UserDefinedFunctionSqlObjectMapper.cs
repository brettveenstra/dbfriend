// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="UserDefinedFunctionSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UserDefinedFunctionSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class UserDefinedFunctionSqlObjectMapper : IUserDefinedFunctionSqlObjectMapper
    {
        #region IUserDefinedFunctionSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(UserDefinedFunction from)
        {
            return new MsSqlUserDefinedFunction(from);
        }

        #endregion
    }
}