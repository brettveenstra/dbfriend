// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TableMsSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the TableMsSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Core.Provider.MsSql.Mappers
{
    using DbFriend.Core.Provider.MsSql.Adapters;

    /// <summary>
    /// </summary>
    public class TableMsSqlObjectMapper : ITableAdapterMsSqlObjectMapper
    {

        /// <summary>
        /// </summary>
        private IMsSqlStatementsTransformer transformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableMsSqlObjectMapper"/> class. 
        /// </summary>
        /// <param name="transformer">
        /// The transformer.
        /// </param>
        /// <param name="dependencyRepository">
        /// The dependency repository.
        /// </param>
        public TableMsSqlObjectMapper(IMsSqlStatementsTransformer transformer)
        {
            this.transformer = transformer;
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
            return new MsSqlTable(from, this.transformer);
        }

        #endregion
    }
}