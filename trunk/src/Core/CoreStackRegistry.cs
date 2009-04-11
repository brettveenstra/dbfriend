// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="CoreStackRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the CoreStackRegistry type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Core
{
    using DbFriend.Core.Generator;
    using DbFriend.Core.Provider.MsSql;
    using DbFriend.Core.Provider.MsSql.Adapters;
    using DbFriend.Core.Provider.MsSql.Mappers;

    using StructureMap.Configuration.DSL;

    /// <summary>
    /// </summary>
    public class CoreStackRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreStackRegistry"/> class.
        /// This is for simple, straight-forward Factory operations.
        /// </summary>
        public CoreStackRegistry()
        {
            this.ForRequestedType<IMsSqlDatabaseConnectionAdapter>().TheDefault.Is.OfConcreteType<MsSqlDatabaseConnectionAdapter>();

            this.ForRequestedType<IDbScriptGenerator>().TheDefaultIsConcreteType<DbScriptGenerator>();

            this.ForRequestedType<IStoredProcedureAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<StoredProcedureIDbScriptObjectMapper>();
            this.ForRequestedType<ITableAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<TableMsSqlObjectMapper>();
            this.ForRequestedType<IViewAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<ViewMsSqlObjectMapper>();
            this.ForRequestedType<IUserDefinedFunctionAdapterSqlObjectMapper>().TheDefaultIsConcreteType<UserDefinedFunctionSqlObjectMapper>();

            this.ForRequestedType<IVelocityFileGenerator>().TheDefaultIsConcreteType<VelocityFileGenerator>();

            this.ForRequestedType<IMsSqlStatementsTransformer>().TheDefaultIsConcreteType<MsSqlStatementsTransformer>();
            this.ForRequestedType<IDependencyTreeNodeAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<DependencyTreeNodeMsSqlObjectMapper>();

            this.ForRequestedType<IMsSqlDependencyRepository>().TheDefaultIsConcreteType<MsSqlDependencyRepository>();
        }
    }
}