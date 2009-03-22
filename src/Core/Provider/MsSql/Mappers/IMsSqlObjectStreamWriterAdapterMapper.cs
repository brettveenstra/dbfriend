// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IMsSqlObjectStreamWriterAdapterMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IMsSqlObjectStreamWriterAdapterMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public interface IMsSqlObjectStreamWriterAdapterMapper : IMapper<IMsSqlObject, IDbObjectStreamWriterAdapter>
    {
    }
}