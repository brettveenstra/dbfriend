// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlConnectionSettingsBase.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlConnectionSettingsBase type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    /// <summary>
    /// </summary>
    public abstract class MsSqlConnectionSettingsBase : IMsSqlConnectionSettings
    {
        /// <summary>
        /// </summary>
        private string databaseName;

        /// <summary>
        /// </summary>
        private MsSqlCredentialMethod method = MsSqlCredentialMethod.Integrated;

        /// <summary>
        /// </summary>
        private string password;

        /// <summary>
        /// </summary>
        private string serverInstance;

        /// <summary>
        /// </summary>
        private string userName;

        #region IMsSqlConnectionSettings Members

        /// <summary>
        /// Gets Method for Credentials.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public abstract MsSqlCredentialMethod Method { get; }

        /// <summary>
        /// Gets ServerInstance.
        /// </summary>
        /// <value>
        /// The server instance.
        /// </value>
        public string ServerInstance
        {
            get
            {
                return this.serverInstance;
            }

            protected set
            {
                this.serverInstance = value;
            }
        }

        /// <summary>
        /// Gets AdoNetConnectionString.
        /// </summary>
        /// <value>
        /// The ADODB.NET formatted connection string.
        /// </value>
        public string AdoNetConnectionString
        {
            get
            {
                if (this.method == MsSqlCredentialMethod.SqlUser)
                {
                    return string.Format(
                            "Data Source={0};Initial Catalog={1};User Id={2};Password={3}",
                            this.serverInstance,
                            this.databaseName,
                            this.userName,
                            this.Password);
                }

                return "Data Source=" + this.serverInstance + ";Initial Catalog=" + this.databaseName + ";Integrated Security=True;";
            }
        }

        /// <summary>
        /// Gets UserName.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName
        {
            get
            {
                return this.userName;
            }

            protected set
            {
                this.userName = value;
            }
        }

        /// <summary>
        /// Gets Password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                return this.password;
            }

            protected set
            {
                this.password = value;
            }
        }

        /// <summary>
        /// Gets DatabaseName.
        /// </summary>
        /// <value>
        /// The database name.
        /// </value>
        public string DatabaseName
        {
            get
            {
                return this.databaseName;
            }

            protected set
            {
                this.databaseName = value;
            }
        }

        #endregion
    }
}