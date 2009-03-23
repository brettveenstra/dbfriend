// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IntegrationTestConnectionSettings.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IntegrationTestConnectionSettings type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Testing.Integration.Properties;

namespace DbFriend.Testing.Integration
{
    /// <summary>
    /// </summary>
    public class IntegrationTestConnectionSettings : MsSqlBasicConnectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestConnectionSettings"/> class.
        /// </summary>
        public IntegrationTestConnectionSettings()
                : this(
                        Settings.Default.server,
                        Settings.Default.userid,
                        Settings.Default.pwd,
                        Settings.Default.db)
        {
            Debug.WriteLine(string.Format("ServerInstance: {0}", ServerInstance));
            Debug.WriteLine(string.Format("Database: {0}", DatabaseName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestConnectionSettings"/> class.
        /// </summary>
        /// <param name="serverInstance">
        /// The server instance.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="databaseName">
        /// The database name.
        /// </param>
        private IntegrationTestConnectionSettings(
                string serverInstance,
                string userName,
                string password,
                string databaseName)
                : base(serverInstance, userName, password, databaseName)
        {
        }
    }
}