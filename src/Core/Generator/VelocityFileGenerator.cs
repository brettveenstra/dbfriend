// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="VelocityFileGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the VelocityFileGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace DbFriend.Core.Generator
{
    using NVelocity;

    public class VelocityFileGenerator : IVelocityFileGenerator
    {
        private readonly IVelocityFileTransformer velocityTransformer;

        public VelocityFileGenerator(IVelocityFileTransformer velocityTransformer)
        {
            this.velocityTransformer = velocityTransformer;
        }

        public void Generate(string templatePath, string outputFile, VelocityContext velocityContext)
        {
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            using (var writer = File.CreateText(outputFile))
            {
                writer.WriteLine(velocityTransformer.Transform(templatePath, velocityContext));
                writer.Close();
            }
        }
    }
}