namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class ScriptedStoredProcedure : IScriptedStoredProcedure
    {
        /// <summary>
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptedStoredProcedure"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public ScriptedStoredProcedure(string name)
        {
            this.name = name;
        }

        #region IScriptedStoredProcedure Members

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