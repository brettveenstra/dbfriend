using System.Collections.Generic;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public class DependencyWalkerAdapter : IDependencyWalkerAdapter
    {
        #region IDependencyWalkerAdapter Members

        List<IDependencyTreeNodeAdapter> IDependencyWalkerAdapter.DiscoveredDependencies(IMsSqlObject baseObject)
        {
            Urn urn = new Urn(baseObject.UrnString);

            DependencyWalker dependencyWalker = new DependencyWalker();
            DependencyTree dependencyTree = dependencyWalker.DiscoverDependencies(new[] {urn}, DependencyType.Children);
            List<IDependencyTreeNodeAdapter> list = new List<IDependencyTreeNodeAdapter>();
            DependencyTreeNode firstChild = dependencyTree.FirstChild;

            list.Add(new DependencyTreeNodeAdapter(firstChild));

            for (int i = 0; i < dependencyTree.FirstChild.NumberOfSiblings; i++)
            {
                list.Add(new DependencyTreeNodeAdapter(firstChild.NextSibling));
            }

            return list;
        }

        #endregion
    }
}