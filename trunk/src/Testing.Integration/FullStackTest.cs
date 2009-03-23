// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="FullStackTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the FullStackTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics;
using System.IO;
using DbFriend.Core;
using DbFriend.Core.Generator;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;
using DbFriend.Core.Graph;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Core.Provider.MsSql.Mappers;
using DbFriend.Testing.Integration.Properties;
using MbUnit.Framework;
using StructureMap;

namespace DbFriend.Testing.Integration
{
    /// <summary>
    /// </summary>
    [TestFixture]
    public class FullStackTest
    {
        /// <summary>
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string tempPath = Path.GetTempPath();

            string finalOutputPath = Path.Combine(Path.Combine(tempPath, "DbFriend"), Settings.Default.db);

            ObjectFactory.Initialize(
                x =>
                    {
                        x.AddRegistry<CoreStackRegistry>();

//                        x.AddRegistry(new MsSqlServerRegistry(
//                                          Settings.Default.server,
//                                          Settings.Default.db));

                        x.AddRegistry(new MsSqlServerRegistry(
                                          Settings.Default.server,
                                          Settings.Default.db,
                                          Settings.Default.userid,
                                          Settings.Default.pwd
                                          ));

                        x.ForRequestedType<IDbScriptFolderConfigurationSetting>().TheDefault
                            .Is.OfConcreteType<DbScriptFolderConfigurationSetting>()
                            .WithCtorArg("outputFolder")
                            .EqualTo(finalOutputPath);

                        x.ForRequestedType<IDbScriptOutputPipeline>()
                            .TheDefaultIsConcreteType<DbScriptOutputFolderPipeline>();

                        x.ForRequestedType<IDbSolutionGenerator>()
                            .TheDefaultIsConcreteType<Vs2008SolutionGenerator>();

                        x.ForRequestedType<IDbScriptFolderManager>()
                            .TheDefaultIsConcreteType<DbScriptFolderManager>();

                        x.ForRequestedType<IMsSqlStoredProcStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                            <MsSqlStoredProcStreamWriterAdapterMapper>();
                        x.ForRequestedType<IMsSqlTableStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                            <MsSqlTableStreamWriterAdapterMapper>();
                        x.ForRequestedType<IMsSqlViewStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                            <MsSqlViewStreamWriterAdapterMapper>();
                        x.ForRequestedType<IMsSqlFunctionStreamWriterAdapterMapper>().TheDefaultIsConcreteType
                            <MsSqlFunctionStreamWriterAdapterMapper>();

                        string codeBase = Directory.GetCurrentDirectory();
                        string resourceTemplates = Path.Combine("Resources", "Templates");

                        x.ForRequestedType<IConfiguredVelocityEngine>().TheDefault
                            .Is.OfConcreteType<ConfiguredVelocityEngine>()
                            .WithCtorArg("templateDirectory").EqualTo(
                            Path.Combine(
                                codeBase,
                                resourceTemplates));

                        x.ForRequestedType<IVelocityFileTransformer>().TheDefaultIsConcreteType<VelocityFileTransformer>();

                    });

            Debug.WriteLine(ObjectFactory.WhatDoIHave());

            ObjectFactory.AssertConfigurationIsValid();
        }

        /// <summary>
        /// </summary>

        [Test]
        [Ignore]
        public void Test_The_Stack()
        {
            IDbScriptGenerator scriptGenerator = ObjectFactory.GetInstance<IDbScriptGenerator>();
            scriptGenerator.ScriptDb(update => Debug.WriteLine(update.UpdateMessage));

            IDbSolutionGenerator solutionGenerator = ObjectFactory.GetInstance<IDbSolutionGenerator>();
            solutionGenerator.Generate(x => Debug.WriteLine(x));
        }
    }
}