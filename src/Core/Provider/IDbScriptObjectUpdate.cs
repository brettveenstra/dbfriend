// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbScriptObjectUpdate.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbScriptObjectUpdate type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public interface IDbScriptObjectUpdate
    {
        /// <summary>
        /// Gets UpdateMessage.
        /// </summary>
        /// <value>
        /// The update message.
        /// </value>
        string UpdateMessage { get; }
    }
}