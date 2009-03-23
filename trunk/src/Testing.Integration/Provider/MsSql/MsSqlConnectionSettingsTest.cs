using DbFriend.Core.Provider.MsSql;
using DbFriend.Core.Provider.MsSql.Adapters;
using DbFriend.Testing.Integration.Properties;
using MbUnit.Framework;

namespace DbFriend.Testing.Integration.Provider.MsSql
{
    [TestFixture]
    public class MsSqlConnectionSettingsTest
    {
        [Test]
        public void Can_Make_Connections_With_User_And_Password()
        {
            IMsSqlConnectionSettings sut = new MsSqlBasicConnectionSettings(
                Settings.Default.server,
                Settings.Default.userid,
                Settings.Default.pwd,
                Settings.Default.db);

            Assert_Can_Connect_Using_Settings(sut);
        }

        [Test]
        public void Can_Make_Connections_Using_Integrated_Security()
        {
            IMsSqlConnectionSettings sut = new MsSqlIntegratedConnectionSettings(Settings.Default.server,
                                                                                 Settings.Default.db);
            Assert_Can_Connect_Using_Settings(sut);
        }

        private void Assert_Can_Connect_Using_Settings(IMsSqlConnectionSettings sut)
        {
            IMsSqlDatabaseConnectionAdapter adapter = new MsSqlDatabaseConnectionAdapter(sut, null, null, null, null);
            adapter.Connect();

            Assert.IsTrue(adapter.IsOpen);
        }
    }
}