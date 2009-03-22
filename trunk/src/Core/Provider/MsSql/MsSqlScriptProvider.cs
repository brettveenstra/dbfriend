// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlScriptProvider.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlScriptProvider type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider.MsSql
{
    /// <summary>
    /// </summary>
    public class MsSqlScriptProvider : IMsSqlScriptProvider
    {
        /// <summary>
        /// </summary>
        private Action<IDbScriptObjectUpdate> notifyAction;

        /// <summary>
        /// </summary>
        private IDbScriptOutputPipeline pipeline;

        /// <summary>
        /// </summary>
        private IDbScriptFolderConfigurationSetting setting;

        #region IMsSqlScriptProvider Members

        /// <summary>
        /// </summary>
        /// <param name="outputPipeline">
        /// The output pipeline.
        /// </param>
        /// <returns>
        /// </returns>
        public IDbScriptProvider ScriptUsing(IDbScriptOutputPipeline outputPipeline)
        {
            pipeline = outputPipeline;
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="updateAction">
        /// The update action.
        /// </param>
        /// <returns>
        /// </returns>
        public IDbScriptProvider NotifyingActionsWith(Action<IDbScriptObjectUpdate> updateAction)
        {
            notifyAction = updateAction;
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="configurationSetting">
        /// The configuration setting.
        /// </param>
        /// <returns>
        /// </returns>
        public IDbScriptProvider WithSetting(IDbScriptFolderConfigurationSetting configurationSetting)
        {
            setting = configurationSetting;
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="databaseToScript">
        /// The database to script.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public void ForTheDatabase(IDatabase databaseToScript)
        {
            if (pipeline == null)
            {
                throw new InvalidOperationException("Must initialize a IDbScriptOutputPipeline");
            }

            if (setting == null)
            {
                throw new InvalidOperationException("Must initialize a IDbScriptFolderConfigurationSetting");
            }

            databaseToScript.ScriptTo(pipeline, notifyAction);
        }

        #endregion
    }
}