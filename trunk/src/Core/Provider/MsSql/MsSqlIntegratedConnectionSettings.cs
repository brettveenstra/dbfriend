namespace DbFriend.Core.Provider.MsSql
{
    public class MsSqlIntegratedConnectionSettings : MsSqlConnectionSettingsBase
    {
        public MsSqlIntegratedConnectionSettings(string serverInstance, string databaseName)
        {
            this.serverInstance = serverInstance;
            this.databaseName = databaseName;
        }

        public override MsSqlCredentialMethod Method
        {
            get { return MsSqlCredentialMethod.Integrated; }
        }
    }
}