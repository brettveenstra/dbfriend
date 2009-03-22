namespace DbFriend.Core.Provider.MsSql
{
    public abstract class MsSqlConnectionSettingsBase : IMsSqlConnectionSettings
    {
        /// <summary>
        /// </summary>
        protected string databaseName;

        /// <summary>
        /// </summary>
        protected MsSqlCredentialMethod method;

        protected string password;

        /// <summary>
        /// </summary>
        protected string serverInstance;

        protected string userName;

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
            get { return serverInstance; }
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
                if (method == MsSqlCredentialMethod.SqlUser)
                {
                    return string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", serverInstance,
                                         databaseName, userName, Password);
                }
                return "Data Source=" + serverInstance + ";Initial Catalog=" + databaseName +
                       ";Integrated Security=True;";
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
            get { return userName; }
        }

        /// <summary>
        /// Gets Password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get { return password; }
        }

        /// <summary>
        /// Gets DatabaseName.
        /// </summary>
        /// <value>
        /// The database name.
        /// </value>
        public string DatabaseName
        {
            get { return databaseName; }
        }

        #endregion
    }
}