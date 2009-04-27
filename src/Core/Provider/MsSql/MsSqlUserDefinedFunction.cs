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
        /// <summary>
        /// </summary>
        private readonly IMsSqlStatementsTransformer transformer;

        /// <summary>
        /// </summary>
        private readonly IUserDefinedFunctionAdapter userDefinedFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlUserDefinedFunction"/> class.
        /// </summary>
        /// <param name="userDefinedFunction">
        /// The user defined function.
        /// </param>
        /// <param name="transformer">
        /// </param>
        /// <param name="mssqlDependencyRepository">
        /// </param>
        public MsSqlUserDefinedFunction(
                IUserDefinedFunctionAdapter userDefinedFunction,
                IMsSqlStatementsTransformer transformer)
        {
            this.userDefinedFunction = userDefinedFunction;
            this.transformer = transformer;
        }

        #region IMsSqlObject Members

        public int Id
        {
            get { throw new NotImplementedException(); }
        }

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

        /// <summary>
        /// Gets Type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type
        {
            get { return "function"; }
        }

        public string Schema
        {
            get { throw new NotImplementedException(); }
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

        /// <summary>
        /// Gets UrnString.
        /// </summary>
        /// <value>
        /// The urn string.
        /// </value>
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

        public int Compare(IDbObject x, IDbObject y)
        {
            throw new NotImplementedException();
        }

        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
        }
    }
}