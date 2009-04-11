// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDatabaseConectionAdapterTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDatabaseConectionAdapterTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Integration.Provider.MsSql.Adapters
{
    using Core.Provider.MsSql;
    using Core.Provider.MsSql.Adapters;

    using MbUnit.Framework;

    using StructureMap;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class MsSqlDatabaseConectionAdapterTest
    {
        /// <summary>
        /// </summary>
        [SetUp]
        public void Setup()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new IntegrationTestRegistry(MsSqlCredentialMethod.SqlUser)));
        }

        /// <summary>
        /// </summary>
        [Test]
        public void Uses_Disposable_Connection()
        {
            IMsSqlDatabaseConnectionAdapter sut = ObjectFactory.GetInstance<IMsSqlDatabaseConnectionAdapter>();

            using (sut)
            {
                sut.Connect();
                Assert.IsTrue(sut.IsOpen);
            }

            Assert.IsFalse(sut.IsOpen);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void Disconnect_Flags_IsOpen_False()
        {
            IMsSqlDatabaseConnectionAdapter sut = ObjectFactory.GetInstance<IMsSqlDatabaseConnectionAdapter>();

            sut.Connect();
            Assert.IsTrue(sut.IsOpen);

            sut.Disconnect();
            Assert.IsFalse(sut.IsOpen);
        }
    }
}