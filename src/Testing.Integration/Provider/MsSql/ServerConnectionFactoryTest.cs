using DbFriend.Core.Provider.MsSql;
using DbFriend.Testing.Extensions;
using MbUnit.Framework;
using Microsoft.SqlServer.Management.Common;
using StructureMap;

namespace DbFriend.Testing.Integration.Provider.MsSql
{
    [TestFixture]
    public class ServerConnectionFactoryTest
    {
        [Test]
        public void CanCreateServerConnection()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new IntegrationTestRegistry(MsSqlCredentialMethod.SqlUser)));

            IServerConnectionFactory sut = ObjectFactory.GetInstance<IServerConnectionFactory>();

            ServerConnection serverConnection = sut.Create();
            serverConnection.ServerInstance.ShouldBe(Properties.Settings.Default.server);

        }       
    }
}