using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public interface ITableAdapter : ISqlSmoObjectAdapter
    {
        string Name { get; }
        string Owner { get; }
        bool IsSystemObject { get; }
        StringCollection Script(ScriptingOptions options);
    }
}