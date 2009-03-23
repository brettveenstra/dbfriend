using System.Collections.Generic;
using DbFriend.Core.Provider.MsSql;

namespace DbFriend.Core.Graph
{
    public interface IDependencyGraph
    {
        IEnumerable<IMsSqlObject> OrderedDependencies { get; }
    }
}