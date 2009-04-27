// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbScriptProvider.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbScriptProvider type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public interface IDbScriptProvider
    {
        /// <summary>
        /// </summary>
        /// <param name="outputPipeline">
        /// The output target.
        /// </param>
        IDbScriptProvider ScriptUsing(IDbScriptOutputPipeline outputPipeline);

        /// <summary>
        /// </summary>
        /// <param name="updateAction">
        /// The update action.
        /// </param>
        /// <returns>
        /// </returns>
        IDbScriptProvider NotifyingActionsWith(Action<IDbScriptObjectUpdate> updateAction);

        /// <summary>
        /// </summary>
        /// <param name="configurationSetting">
        /// The configuration setting.
        /// </param>
        /// <returns>
        /// </returns>
        IDbScriptProvider WithSetting(IDbScriptFolderConfigurationSetting configurationSetting);

        /// <summary>
        /// </summary>
        /// <param name="databaseScripter">
        /// The database to script.
        /// </param>
        void ForTheDatabase(IDatabaseScripter databaseScripter);
    }
}