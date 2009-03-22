// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptFolderConfigurationSetting.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptFileConfigurationSetting type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Generator.Settings
{
    /// <summary>
    /// </summary>
    public class DbScriptFolderConfigurationSetting : IDbScriptFolderConfigurationSetting
    {
        /// <summary>
        /// </summary>
        private readonly string outputFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbScriptFolderConfigurationSetting"/> class.
        /// </summary>
        /// <param name="outputFolder">
        /// The output folder.
        /// </param>
        public DbScriptFolderConfigurationSetting(string outputFolder)
        {
            this.outputFolder = outputFolder;
        }

        #region IDbScriptFolderConfigurationSetting Members

        /// <summary>
        /// Gets OutputFolder.
        /// </summary>
        /// <value>
        /// The output folder.
        /// </value>
        public string OutputFolder
        {
            get { return outputFolder; }
        }

        #endregion
    }
}