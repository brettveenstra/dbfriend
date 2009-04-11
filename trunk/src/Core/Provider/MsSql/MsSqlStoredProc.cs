// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlStoredProc.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlStoredProc type.
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
    public class MsSqlStoredProc : IMsSqlStoredProc
    {
        /// <summary>
        /// </summary>
        private readonly IMsSqlDependencyRepository dependencyRepository;

        /// <summary>
        /// </summary>
        private readonly IStoredProcedureAdapter storedProcedure;

        /// <summary>
        /// </summary>
        private IMsSqlStatementsTransformer statementTransformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlStoredProc"/> class.
        /// </summary>
        /// <param name="storedProcedure">
        /// The stored procedure.
        /// </param>
        /// <param name="transformer">
        /// </param>
        /// <param name="dependencyRepository">
        /// </param>
        public MsSqlStoredProc(
                IStoredProcedureAdapter storedProcedure, IMsSqlStatementsTransformer transformer, IMsSqlDependencyRepository dependencyRepository)
        {
            this.storedProcedure = storedProcedure;
            this.statementTransformer = transformer;
            this.dependencyRepository = dependencyRepository;
        }

        #region IMsSqlStoredProc Members

        /// <summary>
        /// Gets DbObjectName.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return this.storedProcedure.Name;
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
                return "storedprocedure";
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
                return this.storedProcedure.Owner;
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
                foreach (IMsSqlObject sqlObject in this.dependencyRepository.GetDependencies(this))
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
                return this.storedProcedure.Urn;
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

            StringCollection lines = this.storedProcedure.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                this.statementTransformer.Process(line, stringBuilder);
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

            StringCollection lines = this.storedProcedure.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                this.statementTransformer.Process(line, stringBuilder);
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append("GO" + Environment.NewLine + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}