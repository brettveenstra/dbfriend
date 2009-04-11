// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IUserDefinedFunctionAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IUserDefinedFunctionAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    using System.Collections.Specialized;

    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// </summary>
    public interface IUserDefinedFunctionAdapter : ISqlSmoObjectAdapter
    {
        /// <summary>
        /// Gets IsSystemObject.
        /// </summary>
        /// <value>
        /// The is system object.
        /// </value>
        bool IsSystemObject { get; }

        /// <summary>
        /// Gets Name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets Owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        string Owner { get; }

        /// <summary>
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// </returns>
        StringCollection Script(ScriptingOptions options);
    }
}