// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptOutputFolderPipeline.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptOutputFolderPipeline type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace DbFriend.Core.Generator.Targets
{
    /// <summary>
    /// </summary>
    public class DbScriptOutputFolderPipeline : IDbScriptOutputPipeline
    {
        private readonly IDbScriptFolderManager folderManager;
        private readonly List<IDbObjectStreamWriterAdapter> wiredObjects = new List<IDbObjectStreamWriterAdapter>();

        public DbScriptOutputFolderPipeline(IDbScriptFolderManager folderManager)
        {
            this.folderManager = folderManager;
        }

        #region IDbScriptOutputPipeline Members

        public IEnumerable<string> Flush()
        {
            folderManager.Prepare();

            foreach (IDbObjectStreamWriterAdapter writerAdapter in wiredObjects)
            {
                yield return writerAdapter.Write();
            }
        }

        public void WireIn(IDbObjectStreamWriterAdapter adapter)
        {
            wiredObjects.Add(adapter);
        }

        #endregion
    }
}