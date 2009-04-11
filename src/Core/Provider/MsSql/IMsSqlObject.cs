// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IMsSqlObject.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IMsSqlObject type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    using System.Collections.Generic;

    /// <summary>
    /// </summary>
    public interface IMsSqlObject : IDbScriptObject
    {
        /// <summary>
        /// Gets Owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        string Owner { get; }

        /// <summary>
        /// Gets Dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        IEnumerable<IMsSqlObject> Dependencies { get; }

        /// <summary>
        /// Gets UrnString.
        /// </summary>
        /// <value>
        /// The urn string.
        /// </value>
        string UrnString { get; }
    }
}