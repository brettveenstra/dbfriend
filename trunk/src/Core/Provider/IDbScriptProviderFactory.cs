// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbScriptProviderFactory.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbScriptProviderFactory type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public interface IDbScriptProviderFactory
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        IDbScriptProvider Create();
    }
}