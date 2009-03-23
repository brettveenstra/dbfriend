using System;
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql
{
    public class DependencyTreeNodeMsSqlObjectMapper : IDependencyTreeNodeAdapterMsSqlObjectMapper
    {
        #region IDependencyTreeNodeAdapterMsSqlObjectMapper Members

        public IMsSqlObject MapFrom(IDependencyTreeNodeAdapter from)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}