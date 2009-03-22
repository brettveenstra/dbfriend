// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ScriptedTable.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ScriptedTable type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class ScriptedTable : IScriptedTable
    {
        /// <summary>
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptedTable"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public ScriptedTable(string name)
        {
            this.name = name;
        }

        #region IScriptedTable Members

        /// <summary>
        /// Gets Name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name; }
        }

        #endregion
    }
}