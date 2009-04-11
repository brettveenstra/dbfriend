// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IntegrationSmokeTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IntegrationSmokeTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Integration
{
    using System.Data.SqlClient;
    using System.Diagnostics;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using StructureMap;
    using Settings = DbFriend.Testing.Integration.Properties.Settings;

    /// <summary>
    /// </summary>
    public class IntegrationSmokeTest
    {
        /// <summary>
        /// </summary>
        [ValidationMethod]
        public void SetupIntegrationDatabase()
        {
            string connectionString = string.Format(
                "server={0};database={1};user id={2};password={3}",
                Settings.Default.server,
                Settings.Default.db,
                Settings.Default.userid,
                Settings.Default.pwd);

            SqlConnection connection = new SqlConnection(connectionString);

            ServerConnection serverConnection = new ServerConnection(connection);
            serverConnection.Connect();

            Server server = new Server(serverConnection);
            Database database = server.Databases[Settings.Default.db];

            this.CheckIntegrationDatabaseIsSetup(serverConnection);
        }

        /// <summary>
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        private void CheckIntegrationDatabaseIsSetup(ServerConnection connection)
        {
            Debug.WriteLine("Checking Integration Database Connection");

            connection.Connect();
            connection.Disconnect();
        }
    }
}