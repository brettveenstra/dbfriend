// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlUserDefinedFunction.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlUserDefinedFunction type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Core.Provider.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;

    using DbFriend.Core.Provider.MsSql.Adapters;

    using Microsoft.SqlServer.Management.Smo;

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
        /// </summary>
        private IMsSqlDependencyRepository mssqlDependencyRepository;

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
                IMsSqlStatementsTransformer transformer,
                IMsSqlDependencyRepository mssqlDependencyRepository)
        {
            this.userDefinedFunction = userDefinedFunction;
            this.transformer = transformer;
            this.mssqlDependencyRepository = mssqlDependencyRepository;
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
            get
            {
                return this.userDefinedFunction.Name;
            }
        }

        /// <summary>
        /// Gets Type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type
        {
            get
            {
                return "function";
            }
        }

        /// <summary>
        /// Gets Owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner
        {
            get
            {
                return this.userDefinedFunction.Owner;
            }
        }

        /// <summary>
        /// Gets Dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        public IEnumerable<IMsSqlObject> Dependencies
        {
            get
            {
                foreach (IMsSqlObject sqlObject in this.mssqlDependencyRepository.GetDependencies(this))
                {
                    yield return sqlObject;
                }
            }
        }

        /// <summary>
        /// Gets UrnString.
        /// </summary>
        /// <value>
        /// The urn string.
        /// </value>
        public string UrnString
        {
            get
            {
                return this.userDefinedFunction.Urn;
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public string Script()
        {
            string dropScript = this.ScriptDrop(new BaselineScriptingOptionsAdapter().Options);

            string createScript = this.ScriptCreate(new BaselineScriptingOptionsAdapter().Options);

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

            StringCollection lines = this.userDefinedFunction.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                this.transformer.Process(line, stringBuilder);
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

            StringCollection lines = this.userDefinedFunction.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                this.transformer.Process(line, stringBuilder);
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append("GO" + Environment.NewLine + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}