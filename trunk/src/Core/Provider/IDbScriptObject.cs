// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbScriptObject.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbScriptObject type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public interface IDbScriptObject
    {
        /// <summary>
        /// Gets DbObjectName.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets Type of Object.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        string Script();
    }
}