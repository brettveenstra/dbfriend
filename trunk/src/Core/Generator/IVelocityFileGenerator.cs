// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IVelocityFileGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IVelocityFileGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System.Collections;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public interface IVelocityFileGenerator
    {
        void Generate(string templatePath, string outputFile, Hashtable velocityContext);
    }
}