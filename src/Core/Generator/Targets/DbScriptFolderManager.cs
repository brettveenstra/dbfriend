using System.IO;
using DbFriend.Core.Generator.Settings;

namespace DbFriend.Core.Generator.Targets
{
    public class DbScriptFolderManager : IDbScriptFolderManager
    {
        private readonly IDbScriptFolderConfigurationSetting folderConfigurationSetting;

        public DbScriptFolderManager(IDbScriptFolderConfigurationSetting folderConfigurationSetting)
        {
            this.folderConfigurationSetting = folderConfigurationSetting;
        }

        #region IDbScriptFolderManager Members

        public void Prepare()
        {
            PrepareStartingFolder();
            PrepareScriptTypeFolders();
        }

        #endregion

        private void PrepareScriptTypeFolders()
        {
            string scriptBasePath = Path.Combine(folderConfigurationSetting.OutputFolder, "DbScripts");
            Directory.CreateDirectory(scriptBasePath);

            Directory.CreateDirectory(Path.Combine(scriptBasePath, "SPs"));
            Directory.CreateDirectory(Path.Combine(scriptBasePath, "Tables"));
            Directory.CreateDirectory(Path.Combine(scriptBasePath, "Views"));
            Directory.CreateDirectory(Path.Combine(scriptBasePath, "UDFs"));
        }

        private void PrepareStartingFolder()
        {
            Directory.CreateDirectory(folderConfigurationSetting.OutputFolder);
        }
    }
}