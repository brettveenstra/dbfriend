// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IViewMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IViewMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public interface IViewMsSqlObjectMapper : IMapper<View, IMsSqlObject>
    {
    }
}