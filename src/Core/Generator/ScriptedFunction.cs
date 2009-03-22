// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ScriptedFunction.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ScriptedFunction type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class ScriptedFunction : IScriptedFunction
    {
        /// <summary>
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptedFunction"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public ScriptedFunction(string name)
        {
            this.name = name;
        }

        #region IScriptedFunction Members

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