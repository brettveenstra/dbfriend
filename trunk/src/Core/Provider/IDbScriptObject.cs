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
    public interface IDbScriptObject : IDbObject
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        string Script();
    }
}