// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="CoreStackRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the CoreStackRegistry type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Generator;
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
        /// </summary>
        public CoreStackRegistry()
        {
            ForRequestedType<IMsSqlDatabaseConnectionAdapter>().TheDefault
                    .Is.OfConcreteType<MsSqlDatabaseConnectionAdapter>();

            ForRequestedType<IDbScriptGenerator>().TheDefaultIsConcreteType<DbScriptGenerator>();

            ForRequestedType<IStoredProcedureMsSqlObjectMapper>().TheDefaultIsConcreteType<StoredProcedureIDbScriptObjectMapper>();
            ForRequestedType<ITableMsSqlObjectMapper>().TheDefaultIsConcreteType<TableMsSqlObjectMapper>();
            ForRequestedType<IViewMsSqlObjectMapper>().TheDefaultIsConcreteType<ViewMsSqlObjectMapper>();
            ForRequestedType<IUserDefinedFunctionSqlObjectMapper>().TheDefaultIsConcreteType<UserDefinedFunctionSqlObjectMapper>();

            ForRequestedType<IVelocityFileGenerator>().TheDefaultIsConcreteType<VelocityFileGenerator>();
        }
    }
}