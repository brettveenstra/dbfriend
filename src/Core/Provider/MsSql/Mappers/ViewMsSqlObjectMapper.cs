// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ViewMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ViewMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class ViewMsSqlObjectMapper : IViewAdapterMsSqlObjectMapper
    {
        #region IViewAdapterMsSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(IViewAdapter from)
        {
            return new MsSqlView(from);
        }

        #endregion
    }
}