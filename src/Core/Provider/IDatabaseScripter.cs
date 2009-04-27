// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDatabaseScripter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDatabaseScripter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System;
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public interface IDatabaseScripter
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