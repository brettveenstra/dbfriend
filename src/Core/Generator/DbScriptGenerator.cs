// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;
using DbFriend.Core.Provider;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class DbScriptGenerator : IDbScriptGenerator
    {
        /// <summary>
        /// </summary>
        private readonly IDatabase database;

        /// <summary>
        /// </summary>
        private readonly IDbScriptOutputPipeline outputPipeline;

        /// <summary>
        /// </summary>
        private readonly IDbScriptProvider provider;

        /// <summary>
        /// </summary>
        private readonly IDbScriptFolderConfigurationSetting setting;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbScriptGenerator"/> class.
        /// </summary>
        /// <param name="outputPipeline">
        /// The output target.
        /// </param>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="database">
        /// </param>
        /// <param name="setting">
        /// </param>
        public DbScriptGenerator(IDbScriptProvider provider,
                                 IDatabase database,
                                 IDbScriptOutputPipeline outputPipeline,
                                 IDbScriptFolderConfigurationSetting setting)
        {
            this.provider = provider;
            this.setting = setting;
            this.database = database;
            this.outputPipeline = outputPipeline;
        }

        #region IDbScriptGenerator Members

        /// <summary>
        /// </summary>
        /// <param name="updateAction">
        /// The update action.
        /// </param>
        public void ScriptDb(Action<IDbScriptObjectUpdate> updateAction)
        {
            provider
                    .ScriptUsing(outputPipeline)
                    .NotifyingActionsWith(updateAction)
                    .WithSetting(setting)
                    .ForTheDatabase(database);
        }

        #endregion
    }
}