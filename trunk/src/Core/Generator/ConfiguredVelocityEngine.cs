// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ConfiguredVelocityEngine.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ConfiguredVelocityEngine type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class ConfiguredVelocityEngine : IConfiguredVelocityEngine
    {
        /// <summary>
        /// </summary>
        private readonly VelocityEngine velocityEngine = new VelocityEngine();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfiguredVelocityEngine"/> class.
        /// </summary>
        /// <param name="templateDirectory">
        /// The template directory.
        /// </param>
        public ConfiguredVelocityEngine(string templateDirectory)
        {
            velocityEngine.SetProperty(
                    RuntimeConstants.RUNTIME_LOG_LOGSYSTEM_CLASS,
                    "NVelocity.Runtime.Log.NullLogSystem");
            velocityEngine.SetProperty(
                    RuntimeConstants.FILE_RESOURCE_LOADER_PATH,
                    templateDirectory);
            velocityEngine.SetProperty(
                    RuntimeConstants.RESOURCE_MANAGER_CLASS,
                    "NVelocity.Runtime.Resource.ResourceManagerImpl");

            velocityEngine.Init();
        }

        #region IConfiguredVelocityEngine Members

        /// <summary>
        /// </summary>
        /// <param name="templateName">
        /// The template name.
        /// </param>
        /// <param name="velocityContext">
        /// The velocity context.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public void MergeTemplate(string templateName, VelocityContext velocityContext, TextWriter writer)
        {
            velocityEngine.MergeTemplate(templateName, velocityContext, writer);
        }

        #endregion
    }
}