// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IMsSqlConnectionSettings.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IMsSqlConnectionSettings type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Generator.Settings;

namespace DbFriend.Core.Provider.MsSql
{
    /// <summary>
    /// </summary>
    public interface IMsSqlConnectionSettings
    {
        /// <summary>
        /// Gets Method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        MsSqlCredentialMethod Method { get; }

        /// <summary>
        /// Gets UserName.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        string UserName { get; }

        /// <summary>
        /// Gets Password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; }

        /// <summary>
        /// Gets DatabaseName.
        /// </summary>
        /// <value>
        /// The database name.
        /// </value>
        string DatabaseName { get; }

        /// <summary>
        /// Gets ServerInstance.
        /// </summary>
        /// <value>
        /// The server instance.
        /// </value>
        string ServerInstance { get; }

        /// <summary>
        /// Gets AdoNetConnectionString.
        /// </summary>
        /// <value>
        /// The ado net connection string.
        /// </value>
        string AdoNetConnectionString { get; }
    }
}