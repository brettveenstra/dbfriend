// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlTable.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlTable type.
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
    public class MsSqlTable : IMsSqlTable
    {
        /// <summary>
        /// </summary>
        private readonly ITableAdapter table;

        private readonly IMsSqlStatementsTransformer transformer;
        private IMsSqlDependencyRepository dependencyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlTable"/> class.
        /// </summary>
        /// <param name="table">
        /// The table.
        /// </param>
        /// <param name="transformer"></param>
        /// <param name="dependencyRepository"></param>
        public MsSqlTable(
            ITableAdapter table,
            IMsSqlStatementsTransformer transformer,
            IMsSqlDependencyRepository dependencyRepository)
        {
            this.table = table;
            this.transformer = transformer;
            this.dependencyRepository = dependencyRepository;
        }

        #region IMsSqlTable Members

        /// <summary>
        /// Gets Name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return table.Name; }
        }

        public string Type
        {
            get { return "table"; }
        }

        /// <summary>
        /// Gets Owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner
        {
            get { return table.Owner; }
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
            get { return table.Urn; }
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

            StringCollection lines = table.Script(options);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in lines)
            {
                transformer.Process(line, stringBuilder);

                if (line.StartsWith("ALTER TABLE ", StringComparison.OrdinalIgnoreCase))
                {
                    stringBuilder.Append(Environment.NewLine + Environment.NewLine);
                }
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

            StringCollection lines = table.Script(options);
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