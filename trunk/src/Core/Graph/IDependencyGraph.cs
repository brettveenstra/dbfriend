using System.Collections.Generic;
using DbFriend.Core.Provider;

namespace DbFriend.Core.Graph
{
    public interface IDependencyGraph
    {
        IEnumerable<IDbObject> Dependencies { get; }
        IEnumerable<IDbObject> DbObjects { get; }
        void AddDependency(IDbObject child, IDbObject parent);
        void AddDbObject(IDbObject dbObject);
    }
}