// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDependencyRepository.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDependencyRepository type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    using System.Collections.Generic;

    using DbFriend.Core.Provider.MsSql.Adapters;

    /// <summary>
    /// </summary>
    public class MsSqlDependencyRepository : IMsSqlDependencyRepository
    {
        /// <summary>
        /// </summary>
        private IDependencyWalkerAdapter dependencyWalkerAdapter;

        /// <summary>
        /// </summary>
        private IDependencyTreeNodeAdapterMsSqlObjectMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDependencyRepository"/> class. 
        /// </summary>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        /// <param name="dependencyWalkerAdapter">
        /// The dependency walker adapter.
        /// </param>
        public MsSqlDependencyRepository(IDependencyTreeNodeAdapterMsSqlObjectMapper mapper, IDependencyWalkerAdapter dependencyWalkerAdapter)
        {
            this.mapper = mapper;
            this.dependencyWalkerAdapter = dependencyWalkerAdapter;
        }

        #region IMsSqlDependencyRepository Members

        /// <summary>
        /// </summary>
        /// <param name="mssqlObject">
        /// The mssql object.
        /// </param>
        /// <returns>
        /// </returns>
        public IEnumerable<IMsSqlObject> GetDependencies(IMsSqlObject mssqlObject)
        {
            foreach (IDependencyTreeNodeAdapter treeNodeAdapter in this.dependencyWalkerAdapter.DiscoveredDependencies(mssqlObject))
            {
                yield return this.mapper.MapFrom(treeNodeAdapter);
            }
        }

        #endregion
    }
}