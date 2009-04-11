// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlBasicConnectionSettings.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlBasicConnectionSettings type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    /// <summary>
    /// </summary>
    public class MsSqlBasicConnectionSettings : MsSqlConnectionSettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlBasicConnectionSettings"/> class.
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
        public MsSqlBasicConnectionSettings(string serverInstance, string userName, string password, string databaseName)
        {
            this.ServerInstance = serverInstance;
            this.UserName = userName;
            this.Password = password;
            this.DatabaseName = databaseName;
        }

        /// <summary>
        /// Gets Method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public override MsSqlCredentialMethod Method
        {
            get
            {
                return MsSqlCredentialMethod.SqlUser;
            }
        }
    }
}