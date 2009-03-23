// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlStoredProc.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlStoredProc type.
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
    public class MsSqlStoredProc : IMsSqlStoredProc
    {
        private readonly IMsSqlDependencyRepository dependencyRepository;

        /// <summary>
        /// </summary>
        private readonly IStoredProcedureAdapter storedProcedure;

        private IMsSqlStatementsTransformer statementTransformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlStoredProc"/> class.
        /// </summary>
        /// <param name="storedProcedure">
        /// The stored procedure.
        /// </param>
        /// <param name="transformer"></param>
        /// <param name="dependencyRepository"></param>
        public MsSqlStoredProc(IStoredProcedureAdapter storedProcedure, IMsSqlStatementsTransformer transformer,
                               IMsSqlDependencyRepository dependencyRepository)
        {
            this.storedProcedure = storedProcedure;
            statementTransformer = transformer;
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
            get { return storedProcedure.Name; }
        }

        public string Type
        {
            get { return "storedprocedure"; }
        }

        /// <summary>
        /// Gets Owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner
        {
            get { return storedProcedure.Owner; }
        }

        public IEnumerable<IMsSqlObject> Dependencies
        {
            get
            {
                foreach (IMsSqlObject sqlObject in dependencyRepository.GetDependencies(this))
                {
                    yield return sqlObject;
                }
            }
        }

        public string UrnString
        {
            get { return storedProcedure.Urn; }
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

            StringCollection lines = storedProcedure.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                statementTransformer.Process(line, stringBuilder);
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

            StringCollection lines = storedProcedure.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                statementTransformer.Process(line, stringBuilder);
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append("GO" + Environment.NewLine + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}