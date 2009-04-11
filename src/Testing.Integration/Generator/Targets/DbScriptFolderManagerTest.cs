// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptFolderManagerTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptFolderManagerTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Integration.Generator.Targets
{
    using DbFriend.Core.Generator.Targets;
    using DbFriend.Core.Provider.MsSql;

    using MbUnit.Framework;

    using StructureMap;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class DbScriptFolderManagerTest
    {
        /// <summary>
        /// </summary>
        [SetUp]
        public void Setup()
        {
            ObjectFactory.Initialize(
                    x =>
                    {
                        x.AddRegistry(new IntegrationTestRegistry(MsSqlCredentialMethod.SqlUser));
                        x.ForRequestedType<IDbScriptFolderManager>().TheDefault.Is.OfConcreteType<DbScriptFolderManager>();
                    });
        }

        /// <summary>
        /// </summary>
        [Test]
        public void Prepare_Should_Build_DbScript_Folder_Structure()
        {
            IDbScriptFolderManager sut = ObjectFactory.GetInstance<IDbScriptFolderManager>();

            sut.Prepare();
        }
    }
}