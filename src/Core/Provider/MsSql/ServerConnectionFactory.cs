using Microsoft.SqlServer.Management.Common;

namespace DbFriend.Core.Provider.MsSql
{
    public class ServerConnectionFactory : IServerConnectionFactory
    {
        private IMsSqlConnectionSettings settings;

        public ServerConnectionFactory(IMsSqlConnectionSettings settings)
        {
            this.settings = settings;
        }

        #region IServerConnectionFactory Members

        public ServerConnection Create()
        {
            ServerConnection serverConnection;
            if (settings.Method == MsSqlCredentialMethod.SqlUser)
            {
                serverConnection = new ServerConnection(settings.ServerInstance, settings.UserName, settings.Password);
            }
            else
            {
                serverConnection = new ServerConnection(settings.ServerInstance);
            }
            return serverConnection;
        }

        #endregion
    }
}