// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ScriptedView.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ScriptedView type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class ScriptedView : IScriptedView
    {
        /// <summary>
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptedView"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public ScriptedView(string name)
        {
            this.name = name;
        }

        #region IScriptedView Members

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