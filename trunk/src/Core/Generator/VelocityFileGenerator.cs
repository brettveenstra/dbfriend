using System.Collections;
using System.IO;

namespace DbFriend.Core.Generator
{
    public class VelocityFileGenerator : IVelocityFileGenerator
    {
        private readonly IVelocityFileTransformer velocityTransformer;

        public VelocityFileGenerator(IVelocityFileTransformer velocityTransformer)
        {
            this.velocityTransformer = velocityTransformer;
        }

        public void Generate(string templatePath, string outputFile, Hashtable velocityContext)
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