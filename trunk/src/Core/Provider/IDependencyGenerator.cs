using DbFriend.Core.Graph;

namespace DbFriend.Core.Provider
{
    public interface IDependencyGenerator
    {
        IDependencyGraph GenerateGraph();
    }
}