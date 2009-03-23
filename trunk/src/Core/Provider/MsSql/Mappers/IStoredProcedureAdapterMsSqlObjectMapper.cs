// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IStoredProcedureAdapterMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IStoredProcedureAdapterMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public interface IStoredProcedureAdapterMsSqlObjectMapper : IMapper<IStoredProcedureAdapter, IMsSqlObject>
    {
    }
}