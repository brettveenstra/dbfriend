// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TableMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the TableMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class TableMsSqlObjectMapper : ITableAdapterMsSqlObjectMapper
    {
        private IMsSqlDependencyRepository dependencyRepository;
        private IMsSqlStatementsTransformer transformer;

        public TableMsSqlObjectMapper(IMsSqlStatementsTransformer transformer,
                                      IMsSqlDependencyRepository dependencyRepository)
        {
            this.transformer = transformer;
            this.dependencyRepository = dependencyRepository;
        }

        #region ITableAdapterMsSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(ITableAdapter from)
        {
            return new MsSqlTable(from, transformer, dependencyRepository);
        }

        #endregion
    }
}