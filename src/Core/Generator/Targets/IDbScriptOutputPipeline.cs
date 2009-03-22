// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbScriptOutputPipeline.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbScriptOutputPipeline type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace DbFriend.Core.Generator.Targets
{
    /// <summary>
    /// </summary>
    public interface IDbScriptOutputPipeline
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerable<string> Flush();

        /// <summary>
        /// </summary>
        /// <param name="adapter">
        /// The adapter.
        /// </param>
        void WireIn(IDbObjectStreamWriterAdapter adapter);
    }
}