// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDependencyTreeNodeAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDependencyTreeNodeAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    /// <summary>
    /// </summary>
    public interface IDependencyTreeNodeAdapter
    {
        /// <summary>
        /// Gets NumberOfSiblings.
        /// </summary>
        /// <value>
        /// The number of siblings.
        /// </value>
        int NumberOfSiblings { get; }

        /// <summary>
        /// Gets NextSibling.
        /// </summary>
        /// <value>
        /// The next sibling.
        /// </value>
        IDependencyTreeNodeAdapter NextSibling { get; }
    }
}