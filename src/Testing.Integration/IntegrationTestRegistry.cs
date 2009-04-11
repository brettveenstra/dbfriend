// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IntegrationTestRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IntegrationTestRegistry type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Integration
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using DbFriend.Core.Generator;
    using DbFriend.Core.Generator.Settings;
    using DbFriend.Core.Generator.Targets;
    using DbFriend.Core.Provider;
    using DbFriend.Core.Provider.MsSql;
    using DbFriend.Core.Provider.MsSql.Adapters;
    using DbFriend.Core.Provider.MsSql.Mappers;
    using DbFriend.Testing.Integration.Properties;

    using StructureMap;
    using StructureMap.Configuration.DSL;

    /// <summary>
    /// </summary>
    public class IntegrationTestRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestRegistry"/> class.
        /// </summary>
        /// <param name="credentialMethod">
        /// The credential Method.
        /// </param>
        public IntegrationTestRegistry(MsSqlCredentialMethod credentialMethod)
        {
            this.ForRequestedType<IMsSqlDatabaseConnectionAdapter>().TheDefault.Is.OfConcreteType<MsSqlDatabaseConnectionAdapter>();

            this.ForRequestedType<IDbScriptFolderConfigurationSetting>().TheDefault.Is.OfConcreteType<DbScriptFolderConfigurationSetting>().
                    WithCtorArg("outputFolder").EqualTo(
                    Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DbFriend"), "_integrationTest"));

            this.ForRequestedType<IDbScriptOutputPipeline>().TheDefault.Is.OfConcreteType<DbScriptOutputFolderPipeline>();

            if (credentialMethod == MsSqlCredentialMethod.SqlUser)
            {
                this.ForRequestedType<IMsSqlConnectionSettings>().TheDefault.Is.OfConcreteType<MsSqlBasicConnectionSettings>().WithCtorArg(
                        "serverInstance").EqualTo(Settings.Default.server).WithCtorArg("userName").EqualTo(Settings.Default.userid).WithCtorArg(
                        "password").EqualTo(Settings.Default.pwd).WithCtorArg("databaseName").EqualTo(Settings.Default.db);
            }
            else
            {
                this.ForRequestedType<IMsSqlConnectionSettings>().TheDefault.Is.OfConcreteType<MsSqlIntegratedConnectionSettings>().WithCtorArg(
                        "serverInstance").EqualTo(Settings.Default.server).WithCtorArg("databaseName").EqualTo(Settings.Default.db);
            }

            this.ForRequestedType<IMsSqlScriptProvider>().TheDefault.Is.OfConcreteType<MsSqlScriptProvider>();

            this.ForRequestedType<IntegrationSmokeTest>().TheDefault.Is.OfConcreteType<IntegrationSmokeTest>();

            this.ForRequestedType<IDatabase>().TheDefault.Is.OfConcreteType<MsSqlDatabase>();

            this.ForRequestedType<IStoredProcedureAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<StoredProcedureIDbScriptObjectMapper>();
            this.ForRequestedType<ITableAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<TableMsSqlObjectMapper>();
            this.ForRequestedType<IViewAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<ViewMsSqlObjectMapper>();
            this.ForRequestedType<IUserDefinedFunctionAdapterSqlObjectMapper>().TheDefaultIsConcreteType<UserDefinedFunctionSqlObjectMapper>();

            this.ForRequestedType<IMsSqlStoredProcStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlStoredProcStreamWriterAdapterMapper>();
            this.ForRequestedType<IMsSqlTableStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlTableStreamWriterAdapterMapper>();
            this.ForRequestedType<IMsSqlViewStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlViewStreamWriterAdapterMapper>();
            this.ForRequestedType<IMsSqlFunctionStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlFunctionStreamWriterAdapterMapper>();

            this.ForRequestedType<IDbScriptFolderManager>().TheDefaultIsConcreteType<DbScriptFolderManager>();

            this.ForRequestedType<IConfiguredVelocityEngine>().TheDefault.Is.OfConcreteType<ConfiguredVelocityEngine>().WithCtorArg(
                    "templateDirectory").EqualTo(Path.Combine("Resources", "Templates"));

            this.ForRequestedType<IVelocityFileTransformer>().TheDefaultIsConcreteType<VelocityFileTransformer>();

            this.ForRequestedType<IMsSqlStatementsTransformer>().TheDefaultIsConcreteType<MsSqlStatementsTransformer>();
            this.ForRequestedType<IDependencyTreeNodeAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<DependencyTreeNodeMsSqlObjectMapper>();

            this.ForRequestedType<IMsSqlDependencyRepository>().TheDefaultIsConcreteType<MsSqlDependencyRepository>();

            this.ForRequestedType<IDependencyWalkerAdapter>().TheDefaultIsConcreteType<DependencyWalkerAdapter>();

            Debug.WriteLine(ObjectFactory.WhatDoIHave());

            ObjectFactory.AssertConfigurationIsValid();
        }
    }
}