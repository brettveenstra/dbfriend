// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ViewMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ViewMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class ViewMsSqlObjectMapper : IViewMsSqlObjectMapper
    {
        #region IViewMsSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(View from)
        {
            return new MsSqlView(from);
        }

        #endregion
    }
}