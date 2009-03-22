// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbFriendGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbFriendGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using DbFriend.Core.Provider;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public interface IDbFriendGenerator
    {
        /// <summary>
        /// </summary>
        void Generate(Action<IDbScriptObjectUpdate> notifyAction);
    }
}