using System;
using DbFriend.Core.Provider;
using QuickGraph;

namespace DbFriend.Core.Graph
{
    public interface IDbDependencyEdge : IEdge<IDbObject>, IEquatable<IDbDependencyEdge>
    {
    }
}