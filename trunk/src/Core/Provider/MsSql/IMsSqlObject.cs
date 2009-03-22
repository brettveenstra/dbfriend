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
    }
}