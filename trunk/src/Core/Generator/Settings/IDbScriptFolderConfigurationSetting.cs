// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbScriptFolderConfigurationSetting.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbScriptFolderConfigurationSetting type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Generator.Settings
{
    /// <summary>
    /// </summary>
    public interface IDbScriptFolderConfigurationSetting
    {
        /// <summary>
        /// Gets OutputFolder.
        /// </summary>
        /// <value>
        /// The output folder.
        /// </value>
        string OutputFolder { get; }
    }
}