using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public interface IStoredProcedureAdapter : ISqlSmoObjectAdapter
    {
        bool IsSystemObject { get; }
        string Name { get; }
        string Owner { get; }
        StringCollection Script(ScriptingOptions options);
    }
}