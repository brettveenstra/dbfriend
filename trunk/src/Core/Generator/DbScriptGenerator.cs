// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Core.Generator
{
    using System;

    using DbFriend.Core.Generator.Settings;
    using DbFriend.Core.Generator.Targets;
    using DbFriend.Core.Provider;

    /// <summary>
    /// </summary>
    public class DbScriptGenerator : IDbScriptGenerator
    {
        /// <summary>
        /// </summary>
        private readonly IDatabaseScripter databaseScripter;

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
        /// <param name="databaseScripter">
        /// </param>
        /// <param name="setting">
        /// </param>
        public DbScriptGenerator(
                IDbScriptProvider provider, IDatabaseScripter databaseScripter, IDbScriptOutputPipeline outputPipeline, IDbScriptFolderConfigurationSetting setting)
        {
            this.provider = provider;
            this.setting = setting;
            this.databaseScripter = databaseScripter;
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
            this.provider.ScriptUsing(this.outputPipeline).NotifyingActionsWith(updateAction).WithSetting(this.setting).ForTheDatabase(this.databaseScripter);
        }

        #endregion
    }
}