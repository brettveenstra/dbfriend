// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlUserDefinedFunction.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlUserDefinedFunction type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using DbFriend.Core.Provider.MsSql.Adapters;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql
{
    /// <summary>
    /// </summary>
    public class MsSqlUserDefinedFunction : IMsSqlObject
    {
        private readonly IMsSqlStatementsTransformer transformer;

        /// <summary>
        /// </summary>
        private readonly IUserDefinedFunctionAdapter userDefinedFunction;

        private IMsSqlDependencyRepository msSqlDependencyRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlUserDefinedFunction"/> class.
        /// </summary>
        /// <param name="userDefinedFunction">
        /// The user defined function.
        /// </param>
        /// <param name="transformer"></param>
        /// <param name="msSqlDependencyRepository"></param>
        public MsSqlUserDefinedFunction(
            IUserDefinedFunctionAdapter userDefinedFunction,
            IMsSqlStatementsTransformer transformer,
            IMsSqlDependencyRepository msSqlDependencyRepository)
        {
            this.userDefinedFunction = userDefinedFunction;
            this.transformer = transformer;
            this.msSqlDependencyRepository = msSqlDependencyRepository;
        }

        #region IMsSqlObject Members

        /// <summary>
        /// Gets Name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return userDefinedFunction.Name; }
        }

        public string Type
        {
            get { return "function"; }
        }

        /// <summary>
        /// Gets Owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner
        {
            get { return userDefinedFunction.Owner; }
        }

        public IEnumerable<IMsSqlObject> Dependencies
        {
            get
            {
                foreach (IMsSqlObject sqlObject in msSqlDependencyRepository.GetDependencies(this))
                {
                    yield return sqlObject;
                }
            }
        }

        public string UrnString
        {
            get { return userDefinedFunction.Urn; }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public string Script()
        {
            string dropScript = ScriptDrop(new BaselineScriptingOptionsAdapter().Options);

            string createScript = ScriptCreate(new BaselineScriptingOptionsAdapter().Options);

            return dropScript + createScript;
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// </returns>
        private string ScriptCreate(ScriptingOptions options)
        {
            options.ScriptDrops = false;
            options.IncludeIfNotExists = false;

            StringCollection lines = userDefinedFunction.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                transformer.Process(line, stringBuilder);
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(Environment.NewLine + "GO" + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// </returns>
        private string ScriptDrop(ScriptingOptions options)
        {
            options.ScriptDrops = true;
            options.IncludeIfNotExists = true;

            StringCollection lines = userDefinedFunction.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                transformer.Process(line, stringBuilder);
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append("GO" + Environment.NewLine + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}