namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public interface IDependencyTreeNodeAdapter
    {
        int NumberOfSiblings { get; }
        IDependencyTreeNodeAdapter NextSibling { get; }
    }
}