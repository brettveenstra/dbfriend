// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbObjectStreamWriterAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbObjectStreamWriterAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Text;
using DbFriend.Core.Provider;

namespace DbFriend.Core.Generator.Targets
{
    /// <summary>
    /// </summary>
    public class DbObjectStreamWriterAdapter : IDbObjectStreamWriterAdapter
    {
        /// <summary>
        /// </summary>
        private readonly IDbScriptObject scriptObject;

        /// <summary>
        /// </summary>
        private readonly string targetPath;

        /// <summary>
        /// </summary>
        private StreamWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbObjectStreamWriterAdapter"/> class.
        /// </summary>
        /// <param name="scriptObject">
        /// The script object.
        /// </param>
        /// <param name="outputFileName">
        /// The output folder.
        /// </param>
        public DbObjectStreamWriterAdapter(IDbScriptObject scriptObject, string outputFileName)
        {
            targetPath = outputFileName;

            this.scriptObject = scriptObject;
        }

        #region IDbObjectStreamWriterAdapter Members

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            writer.Close();
            writer.Dispose();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public string Write()
        {
            using (writer = new StreamWriter(targetPath, false, Encoding.ASCII))
            {
                writer.Write(scriptObject.Script());
                writer.Close();
            }

            return targetPath;
        }

        #endregion
    }
}