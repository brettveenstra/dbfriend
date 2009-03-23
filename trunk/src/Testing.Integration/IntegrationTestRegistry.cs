// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IntegrationTestRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IntegrationTestRegistry type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
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

namespace DbFriend.Testing.Integration
{
    /// <summary>
    /// </summary>
    public class IntegrationTestRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestRegistry"/> class.
        /// </summary>
        public IntegrationTestRegistry(MsSqlCredentialMethod credentialMethod)
        {
            ForRequestedType<IMsSqlDatabaseConnectionAdapter>().TheDefault
                .Is.OfConcreteType<MsSqlDatabaseConnectionAdapter>();

            ForRequestedType<IDbScriptFolderConfigurationSetting>().TheDefault
                .Is.OfConcreteType<DbScriptFolderConfigurationSetting>()
                .WithCtorArg("outputFolder").EqualTo(Path.Combine(
                                                         Path.Combine(
                                                             Environment.GetFolderPath(
                                                                 Environment.SpecialFolder.Personal), "DbFriend"),
                                                         "_integrationTest"));

            ForRequestedType<IDbScriptOutputPipeline>().TheDefault.Is.OfConcreteType<DbScriptOutputFolderPipeline>();

            if (credentialMethod == MsSqlCredentialMethod.SqlUser)
            {
                ForRequestedType<IMsSqlConnectionSettings>().TheDefault.Is.OfConcreteType<MsSqlBasicConnectionSettings>()
                    .WithCtorArg("serverInstance").EqualTo(Settings.Default.server)
                    .WithCtorArg("userName").EqualTo(Settings.Default.userid)
                    .WithCtorArg("password").EqualTo(Settings.Default.pwd)
                    .WithCtorArg("databaseName").EqualTo(Settings.Default.db);
            }
            else
            {
                ForRequestedType<IMsSqlConnectionSettings>().TheDefault.Is.OfConcreteType
                    <MsSqlIntegratedConnectionSettings>()
                    .WithCtorArg("serverInstance").EqualTo(Settings.Default.server)
                    .WithCtorArg("databaseName").EqualTo(Settings.Default.db);
            }


            ForRequestedType<IMsSqlScriptProvider>().TheDefault.Is.OfConcreteType<MsSqlScriptProvider>();

            ForRequestedType<IntegrationSmokeTest>().TheDefault.Is.OfConcreteType<IntegrationSmokeTest>();

            ForRequestedType<IDatabase>().TheDefault.Is.OfConcreteType<MsSqlDatabase>();

            ForRequestedType<IStoredProcedureAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType
                <StoredProcedureIDbScriptObjectMapper>();
            ForRequestedType<ITableAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<TableMsSqlObjectMapper>();
            ForRequestedType<IViewAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<ViewMsSqlObjectMapper>();
            ForRequestedType<IUserDefinedFunctionAdapterSqlObjectMapper>().TheDefaultIsConcreteType
                <UserDefinedFunctionSqlObjectMapper>();

            ForRequestedType<IMsSqlStoredProcStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                <MsSqlStoredProcStreamWriterAdapterMapper>();
            ForRequestedType<IMsSqlTableStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                <MsSqlTableStreamWriterAdapterMapper>();
            ForRequestedType<IMsSqlViewStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                <MsSqlViewStreamWriterAdapterMapper>();
            ForRequestedType<IMsSqlFunctionStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                <MsSqlFunctionStreamWriterAdapterMapper>();

            ForRequestedType<IDbScriptFolderManager>().TheDefaultIsConcreteType<DbScriptFolderManager>();

            ForRequestedType<IConfiguredVelocityEngine>().TheDefault
                .Is.OfConcreteType<ConfiguredVelocityEngine>()
                .WithCtorArg("templateDirectory").EqualTo(Path.Combine("Resources", "Templates"));

            ForRequestedType<IVelocityFileTransformer>().TheDefaultIsConcreteType<VelocityFileTransformer>();

            ForRequestedType<IMsSqlStatementsTransformer>().TheDefaultIsConcreteType<MsSqlStatementsTransformer>();
            ForRequestedType<IDependencyTreeNodeAdapterMsSqlObjectMapper>().TheDefaultIsConcreteType<DependencyTreeNodeMsSqlObjectMapper>();

            ForRequestedType<IMsSqlDependencyRepository>().TheDefaultIsConcreteType<MsSqlDependencyRepository>();

            ForRequestedType<IDependencyWalkerAdapter>().TheDefaultIsConcreteType<DependencyWalkerAdapter>();


            Debug.WriteLine(ObjectFactory.WhatDoIHave());

            ObjectFactory.AssertConfigurationIsValid();
        }
    }
}