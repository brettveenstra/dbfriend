// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core
{
    /// <summary>
    /// </summary>
    /// <typeparam name="From">
    /// </typeparam>
    /// <typeparam name="To">
    /// </typeparam>
    public interface IMapper<From, To>
    {
        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        To MapFrom(From from);
    }
}