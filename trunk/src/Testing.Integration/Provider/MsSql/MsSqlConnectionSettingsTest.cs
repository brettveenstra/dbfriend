// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlConnectionSettingsTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlConnectionSettingsTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Testing.Integration.Provider.MsSql
{
    using DbFriend.Core.Provider.MsSql;
    using DbFriend.Core.Provider.MsSql.Adapters;
    using DbFriend.Testing.Integration.Properties;

    using MbUnit.Framework;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class MsSqlConnectionSettingsTest
    {
        /// <summary>
        /// </summary>
        [Test]
        public void Can_Make_Connections_With_User_And_Password()
        {
            IMsSqlConnectionSettings sut = new MsSqlBasicConnectionSettings(
                    Settings.Default.server, Settings.Default.userid, Settings.Default.pwd, Settings.Default.db);

            this.Assert_Can_Connect_Using_Settings(sut);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void Can_Make_Connections_Using_Integrated_Security()
        {
            IMsSqlConnectionSettings sut = new MsSqlIntegratedConnectionSettings(Settings.Default.server, Settings.Default.db);
            this.Assert_Can_Connect_Using_Settings(sut);
        }

        /// <summary>
        /// </summary>
        /// <param name="sut">
        /// The sut.
        /// </param>
        private void Assert_Can_Connect_Using_Settings(IMsSqlConnectionSettings sut)
        {
            IMsSqlDatabaseConnectionAdapter adapter = new MsSqlDatabaseConnectionAdapter(sut, null, null, null, null);
            adapter.Connect();

            Assert.IsTrue(adapter.IsOpen);
        }
    }
}