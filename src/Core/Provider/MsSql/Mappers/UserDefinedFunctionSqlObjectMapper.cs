// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="UserDefinedFunctionSqlObjectMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UserDefinedFunctionSqlObjectMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class UserDefinedFunctionSqlObjectMapper : IUserDefinedFunctionAdapterSqlObjectMapper
    {
        private IMsSqlDependencyRepository dependencyRepository;
        private IMsSqlStatementsTransformer transformer;

        public UserDefinedFunctionSqlObjectMapper(IMsSqlStatementsTransformer transformer,
                                                  IMsSqlDependencyRepository dependencyRepository)
        {
            this.transformer = transformer;
            this.dependencyRepository = dependencyRepository;
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
            return new MsSqlUserDefinedFunction(from, transformer, dependencyRepository);
        }

        #endregion
    }
}