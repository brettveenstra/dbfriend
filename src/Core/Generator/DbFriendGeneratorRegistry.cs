// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbFriendGeneratorRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbFriendGeneratorRegistry type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.IO;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;
using DbFriend.Core.Provider.MsSql.Mappers;
using StructureMap.Configuration.DSL;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class DbFriendGeneratorRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbFriendGeneratorRegistry"/> class.
        /// </summary>
        /// <param name="finalOutputPath">
        /// The final Output Path.
        /// </param>
        public DbFriendGeneratorRegistry(string finalOutputPath)
        {
            ForRequestedType<IDbScriptFolderConfigurationSetting>().TheDefault
                    .Is.OfConcreteType<DbScriptFolderConfigurationSetting>()
                    .WithCtorArg("outputFolder")
                    .EqualTo(finalOutputPath);

            ForRequestedType<IDbScriptOutputPipeline>().TheDefault
                    .Is.OfConcreteType<DbScriptOutputFolderPipeline>();

            ForRequestedType<IDbSolutionGenerator>().TheDefault
                    .Is.OfConcreteType<Vs2008SolutionGenerator>();

            ForRequestedType<IDbScriptFolderManager>().TheDefaultIsConcreteType<DbScriptFolderManager>();

            ForRequestedType<IMsSqlStoredProcStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlStoredProcStreamWriterAdapterMapper>();
            ForRequestedType<IMsSqlTableStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlTableStreamWriterAdapterMapper>();
            ForRequestedType<IMsSqlViewStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlViewStreamWriterAdapterMapper>();
            ForRequestedType<IMsSqlFunctionStreamWriterAdapterMapper>().TheDefaultIsConcreteType<MsSqlFunctionStreamWriterAdapterMapper>();

            string codeBase = Directory.GetCurrentDirectory();
            string resourceTemplates = Path.Combine("Resources", "Templates");

            ForRequestedType<IConfiguredVelocityEngine>().TheDefault
                    .Is.OfConcreteType<ConfiguredVelocityEngine>()
                    .WithCtorArg("templateDirectory").EqualTo(
                    Path.Combine(
                            codeBase,
                            resourceTemplates));

            ForRequestedType<IVelocityFileTransformer>().TheDefaultIsConcreteType<VelocityFileTransformer>();
        }
    }
}