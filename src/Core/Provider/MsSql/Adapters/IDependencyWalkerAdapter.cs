using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public interface IDependencyWalkerAdapter
    {
        List<IDependencyTreeNodeAdapter> DiscoveredDependencies(IMsSqlObject baseObject);
    }
}