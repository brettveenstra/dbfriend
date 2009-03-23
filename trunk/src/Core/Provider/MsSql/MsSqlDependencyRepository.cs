using System;
using System.Collections.Generic;
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql
{
    public class MsSqlDependencyRepository : IMsSqlDependencyRepository
    {
        private IDependencyWalkerAdapter dependencyWalkerAdapter;
        private IDependencyTreeNodeAdapterMsSqlObjectMapper mapper;

        public MsSqlDependencyRepository(IDependencyTreeNodeAdapterMsSqlObjectMapper mapper,
                                         IDependencyWalkerAdapter dependencyWalkerAdapter)
        {
            this.mapper = mapper;
            this.dependencyWalkerAdapter = dependencyWalkerAdapter;
        }

        public IEnumerable<IMsSqlObject> GetDependencies(IMsSqlObject msSqlObject)
        {
            foreach (IDependencyTreeNodeAdapter treeNodeAdapter in dependencyWalkerAdapter.DiscoveredDependencies(msSqlObject))
            {
                yield return mapper.MapFrom(treeNodeAdapter);
            }
        }
    }
}