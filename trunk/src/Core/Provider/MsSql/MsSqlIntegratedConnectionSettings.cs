// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlIntegratedConnectionSettings.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlIntegratedConnectionSettings type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    /// <summary>
    /// </summary>
    public class MsSqlIntegratedConnectionSettings : MsSqlConnectionSettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlIntegratedConnectionSettings"/> class. 
        /// </summary>
        /// <param name="serverInstance">
        /// The server instance.
        /// </param>
        /// <param name="databaseName">
        /// The database name.
        /// </param>
        public MsSqlIntegratedConnectionSettings(string serverInstance, string databaseName)
        {
            this.ServerInstance = serverInstance;
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
                return MsSqlCredentialMethod.Integrated;
            }
        }
    }
}