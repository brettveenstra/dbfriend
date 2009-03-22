// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IUserDefinedFunctionSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IUserDefinedFunctionSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public interface IUserDefinedFunctionSqlObjectMapper : IMapper<UserDefinedFunction, IMsSqlObject>
    {
    }
}