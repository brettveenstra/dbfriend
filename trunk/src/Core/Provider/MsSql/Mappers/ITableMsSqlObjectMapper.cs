// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ITableMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ITableMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public interface ITableMsSqlObjectMapper : IMapper<Table, IMsSqlObject>
    {
    }
}