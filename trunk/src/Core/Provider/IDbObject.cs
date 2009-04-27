// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbObject.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbObject type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public interface IDbObject : IComparer<IDbObject>, IComparer
    {
        /// <summary>
        /// Gets Id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        int Id { get; }

        /// <summary>
        /// Gets Name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets Type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        string Type { get; }

        /// <summary>
        /// Gets Schema.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        string Schema { get; }
    }
}