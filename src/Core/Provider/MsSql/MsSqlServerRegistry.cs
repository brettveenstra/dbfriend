// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlServerRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlServerRegistry type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    using StructureMap.Configuration.DSL;

    /// <summary>
    /// </summary>
    public class MsSqlServerRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlServerRegistry"/> class.
        /// </summary>
        /// <param name="serverName">
        /// The server name.
        /// </param>
        /// <param name="databaseName">
        /// The database name.
        /// </param>
        public MsSqlServerRegistry(string serverName, string databaseName)
                : this(serverName, databaseName, string.Empty, string.Empty, MsSqlCredentialMethod.Integrated)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlServerRegistry"/> class.
        /// </summary>
        /// <param name="serverName">
        /// The server name.
        /// </param>
        /// <param name="databaseName">
        /// The database name.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public MsSqlServerRegistry(string serverName, string databaseName, string userName, string password)
                : this(serverName, databaseName, userName, password, MsSqlCredentialMethod.SqlUser)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlServerRegistry"/> class.
        /// </summary>
        /// <param name="serverName">
        /// The server name.
        /// </param>
        /// <param name="databaseName">
        /// The database name.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="credentialMethod">
        /// </param>
        private MsSqlServerRegistry(string serverName, string databaseName, string userName, string password, MsSqlCredentialMethod credentialMethod)
        {
            if (credentialMethod == MsSqlCredentialMethod.Integrated)
            {
                this.ForRequestedType<IMsSqlConnectionSettings>().TheDefault.Is.OfConcreteType<MsSqlIntegratedConnectionSettings>().WithCtorArg(
                        "serverInstance").EqualTo(serverName).WithCtorArg("databaseName").EqualTo(databaseName);
            }
            else
            {
                this.ForRequestedType<IMsSqlConnectionSettings>().TheDefault.Is.OfConcreteType<MsSqlBasicConnectionSettings>().WithCtorArg("method").
                        EqualTo(credentialMethod).WithCtorArg("userName").EqualTo(userName).WithCtorArg("password").EqualTo(password).WithCtorArg(
                        "serverInstance").EqualTo(serverName).WithCtorArg("databaseName").EqualTo(databaseName);
            }

            this.ForRequestedType<IDbScriptProvider>().TheDefault.Is.OfConcreteType<MsSqlScriptProvider>();

            this.ForRequestedType<IDatabase>().TheDefaultIsConcreteType<MsSqlDatabase>();
        }
    }
}