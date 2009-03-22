// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDatabase.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDatabase type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System;
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// </summary>
        /// <param name="outputPipeline">
        /// The output target.
        /// </param>
        /// <param name="notifyAction">
        /// </param>
        void ScriptTo(IDbScriptOutputPipeline outputPipeline, Action<IDbScriptObjectUpdate> notifyAction);
    }
}