// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptObjectUpdate.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptObjectUpdate type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider
{
    /// <summary>
    /// </summary>
    public class DbScriptObjectUpdate : IDbScriptObjectUpdate
    {
        /// <summary>
        /// </summary>
        private readonly string updateMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbScriptObjectUpdate"/> class.
        /// </summary>
        /// <param name="updateMessage">
        /// The update message.
        /// </param>
        public DbScriptObjectUpdate(string updateMessage)
        {
            this.updateMessage = updateMessage;
        }

        #region IDbScriptObjectUpdate Members

        /// <summary>
        /// Gets UpdateMessage.
        /// </summary>
        /// <value>
        /// The update message.
        /// </value>
        public string UpdateMessage
        {
            get { return updateMessage; }
        }

        #endregion
    }
}