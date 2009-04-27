// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Integration
{
    using System.Diagnostics;

    using DbFriend.Core.Generator;
    using DbFriend.Core.Generator.Settings;
    using DbFriend.Core.Generator.Targets;
    using DbFriend.Core.Provider;
    using DbFriend.Core.Provider.MsSql;

    using MbUnit.Framework;

    using StructureMap;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class DbScriptTest
    {
        /// <summary>
        /// </summary>
        [Test]
        public void Script_The_Database_To_Folder()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new IntegrationTestRegistry(MsSqlCredentialMethod.SqlUser)));
            IDbScriptGenerator generator = new DbScriptGenerator(
                    ObjectFactory.GetInstance<IMsSqlScriptProvider>(),
                    ObjectFactory.GetInstance<IDatabaseScripter>(),
                    ObjectFactory.GetInstance<IDbScriptOutputPipeline>(),
                    ObjectFactory.GetInstance<IDbScriptFolderConfigurationSetting>());

            generator.ScriptDb(a => Debug.WriteLine(a.UpdateMessage));
        }

        /// <summary>
        /// </summary>
        [Test]
        public void Script_The_Database_To_Folder_IntegratedSecurity()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new IntegrationTestRegistry(MsSqlCredentialMethod.Integrated)));
            IDbScriptGenerator generator = new DbScriptGenerator(
                    ObjectFactory.GetInstance<IMsSqlScriptProvider>(),
                    ObjectFactory.GetInstance<IDatabaseScripter>(),
                    ObjectFactory.GetInstance<IDbScriptOutputPipeline>(),
                    ObjectFactory.GetInstance<IDbScriptFolderConfigurationSetting>());

            generator.ScriptDb(a => Debug.WriteLine(a.UpdateMessage));
        }
    }
}