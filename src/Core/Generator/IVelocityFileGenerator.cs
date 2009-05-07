// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IVelocityFileGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IVelocityFileGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Generator
{
    using NVelocity;

    /// <summary>
    /// </summary>
    public interface IVelocityFileGenerator
    {
        void Generate(string templatePath, string outputFile, VelocityContext velocityContext);
    }
}