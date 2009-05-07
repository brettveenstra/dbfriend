// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="VelocityFileTransformer.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the VelocityFileTransformer type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.IO;
using NVelocity;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class VelocityFileTransformer : IVelocityFileTransformer
    {
        /// <summary>
        /// </summary>
        private readonly IConfiguredVelocityEngine configuredEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="VelocityFileTransformer"/> class.
        /// </summary>
        /// <param name="configuredEngine">
        /// The configured engine.
        /// </param>
        public VelocityFileTransformer(IConfiguredVelocityEngine configuredEngine)
        {
            this.configuredEngine = configuredEngine;
        }

        #region IVelocityFileTransformer Members

        /// <summary>
        /// </summary>
        /// <param name="velocityTemplatePath">
        /// The velocity template path.
        /// </param>
        /// <param name="velocityContext">
        /// The velocity context.
        /// </param>
        /// <returns>
        /// </returns>
        public string Transform(string velocityTemplatePath, VelocityContext velocityContext)
        {
            string output;
            using (StringWriter writer = new StringWriter())
            {
                configuredEngine.MergeTemplate(velocityTemplatePath, velocityContext, writer);
                output = writer.ToString();
            }

            return output;
        }

        #endregion
    }
}