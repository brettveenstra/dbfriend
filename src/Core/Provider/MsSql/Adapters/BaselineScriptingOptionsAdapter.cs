// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="BaselineScriptingOptionsAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the BaselineScriptingOptionsAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    /// <summary>
    /// </summary>
    public class BaselineScriptingOptionsAdapter
    {
        /// <summary>
        /// </summary>
        private readonly ScriptingOptions options = new ScriptingOptions();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaselineScriptingOptionsAdapter"/> class.
        /// </summary>
        public BaselineScriptingOptionsAdapter()
        {
            options.AnsiFile = true;
            options.AppendToFile = false;
            options.ClusteredIndexes = true;
            options.ContinueScriptingOnError = false;
            options.IncludeHeaders = false;
            options.Indexes = true;
            options.NonClusteredIndexes = true;
            options.Permissions = true;
            options.Triggers = true;
            options.DriAll = true;
            options.ExtendedProperties = true;
        }

        /// <summary>
        /// Gets Options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public ScriptingOptions Options
        {
            get { return options; }
        }
    }
}