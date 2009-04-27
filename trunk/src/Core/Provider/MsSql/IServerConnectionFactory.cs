using Microsoft.SqlServer.Management.Common;

namespace DbFriend.Core.Provider.MsSql
{
    public interface IServerConnectionFactory
    {
        ServerConnection Create();
    }
}