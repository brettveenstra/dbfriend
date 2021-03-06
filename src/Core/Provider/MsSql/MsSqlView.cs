// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlView.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlView type.
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
    public class MsSqlView : IMsSqlObject
    {
        /// <summary>
        /// </summary>
        private readonly IViewAdapter view;

        private IMsSqlStatementsTransformer transformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlView"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        public MsSqlView(IViewAdapter view) : this(view, new MsSqlStatementsTransformer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlView"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="statementTransformer"></param>
        public MsSqlView(IViewAdapter view, IMsSqlStatementsTransformer statementTransformer)
        {
            this.view = view;
            this.transformer = statementTransformer;
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
            get { return view.Name; }
        }

        public string Type
        {
            get { return "view"; }
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
            get { return view.Owner; }
        }

        public IEnumerable<IMsSqlObject> Dependencies
        {
            get { throw new NotImplementedException(); }
        }

        public string UrnString
        {
            get { return view.Urn; }
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

            StringCollection lines = view.Script(options);
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

            StringCollection lines = view.Script(options);
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