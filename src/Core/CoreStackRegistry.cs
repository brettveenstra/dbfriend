// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="CoreStackRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the CoreStackRegistry type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Generator;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Core.Provider.MsSql.Adapters;
using DbFriend.Core.Provider.MsSql.Mappers;
using StructureMap.Configuration.DSL;

namespace DbFriend.Core
{
    /// <summary>
    /// </summary>
    public class CoreStackRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreStackRegistry"/> class.
        /// 
        /// This is for simple, straight-forward Factory operations.
        /// </summary>
        public CoreStackRegistry()
        {
            ForRequestedType<IMsSqlDatabaseConnectionAdapter>().TheDefault
                    .Is.OfConcreteType<MsSqlDatabaseConnectionAdapter>();

            ForRequestedType<IDbScriptGenerator>().TheDefaultIsConcreteType<DbScriptGenerator>();

            ForRequestedType<IStoredProcedureAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<StoredProcedureIDbScriptObjectMapper>();
            ForRequestedType<ITableAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<TableMsSqlObjectMapper>();
            ForRequestedType<IViewAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<ViewMsSqlObjectMapper>();
            ForRequestedType<IUserDefinedFunctionAdapterSqlObjectMapper>().TheDefaultIsConcreteType<UserDefinedFunctionSqlObjectMapper>();

            ForRequestedType<IVelocityFileGenerator>().TheDefaultIsConcreteType<VelocityFileGenerator>();

            ForRequestedType<IMsSqlStatementsTransformer>().TheDefaultIsConcreteType<MsSqlStatementsTransformer>();
            ForRequestedType<IDependencyTreeNodeAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<DependencyTreeNodeMsSqlObjectMapper>();

            ForRequestedType<IMsSqlDependencyRepository>().TheDefaultIsConcreteType<MsSqlDependencyRepository>();

        }
    }
}