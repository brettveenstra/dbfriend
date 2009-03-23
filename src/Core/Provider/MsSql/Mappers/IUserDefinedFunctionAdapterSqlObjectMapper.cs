// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IUserDefinedFunctionAdapterSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IUserDefinedFunctionAdapterSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public interface IUserDefinedFunctionAdapterSqlObjectMapper : IMapper<IUserDefinedFunctionAdapter, IMsSqlObject>
    {
    }
}