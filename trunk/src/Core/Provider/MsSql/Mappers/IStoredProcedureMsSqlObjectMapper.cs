// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IStoredProcedureMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IStoredProcedureMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public interface IStoredProcedureMsSqlObjectMapper : IMapper<StoredProcedure, IMsSqlObject>
    {
    }
}