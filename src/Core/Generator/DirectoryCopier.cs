// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DirectoryCopier.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DirectoryCopier type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.IO;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class DirectoryCopier : IDirectoryCopier
    {
        #region IDirectoryCopier Members

        /// <summary>
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        public void Copy(string source, string target)
        {
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(source);
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(target);

            targetDirectoryInfo.Create();
            foreach (DirectoryInfo sourceChildDirectory in sourceDirectoryInfo.GetDirectories())
            {
                DirectoryInfo targetChildDirectory = targetDirectoryInfo.CreateSubdirectory(sourceChildDirectory.Name);
                Copy(sourceChildDirectory.FullName, targetChildDirectory.FullName);
            }

            foreach (FileInfo sourceFile in sourceDirectoryInfo.GetFiles())
            {
                string destFileName = Path.Combine(targetDirectoryInfo.FullName, sourceFile.Name);
                if (File.Exists(destFileName) == false)
                {
                    sourceFile.CopyTo(destFileName);
                }
            }
        }

        #endregion
    }
}