// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="UserDefinedFunctionSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UserDefinedFunctionSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Core.Provider.MsSql.Mappers
{
    using DbFriend.Core.Provider.MsSql.Adapters;

    /// <summary>
    /// </summary>
    public class UserDefinedFunctionSqlObjectMapper : IUserDefinedFunctionAdapterSqlObjectMapper
    {

        /// <summary>
        /// </summary>
        private IMsSqlStatementsTransformer transformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDefinedFunctionSqlObjectMapper"/> class. 
        /// </summary>
        /// <param name="transformer">
        /// The transformer.
        /// </param>
        /// <param name="dependencyRepository">
        /// The dependency repository.
        /// </param>
        public UserDefinedFunctionSqlObjectMapper(IMsSqlStatementsTransformer transformer)
        {
            this.transformer = transformer;
        }

        #region IUserDefinedFunctionAdapterSqlObjectMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IMsSqlObject MapFrom(IUserDefinedFunctionAdapter from)
        {
            return new MsSqlUserDefinedFunction(from, this.transformer);
        }

        #endregion
    }
}