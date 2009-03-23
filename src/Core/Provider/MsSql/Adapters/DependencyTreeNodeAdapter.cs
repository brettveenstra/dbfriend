using System;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public class DependencyTreeNodeAdapter : IDependencyTreeNodeAdapter
    {
        private readonly DependencyTreeNode firstChild;

        public DependencyTreeNodeAdapter(DependencyTreeNode firstChild)
        {
            this.firstChild = firstChild;
        }

        #region IDependencyTreeNodeAdapter Members

        public int NumberOfSiblings
        {
            get { return firstChild.NumberOfSiblings; }
        }

        public IDependencyTreeNodeAdapter NextSibling
        {
            get { return new DependencyTreeNodeAdapter(firstChild.NextSibling); }
        }

        #endregion
    }
}