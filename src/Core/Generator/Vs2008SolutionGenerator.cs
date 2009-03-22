// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Vs2008SolutionGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Vs2008SolutionGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Provider.MsSql;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class Vs2008SolutionGenerator : IDbSolutionGenerator
    {
        /// <summary>
        /// </summary>
        private readonly IMsSqlConnectionSettings databaseSettings;

        /// <summary>
        /// </summary>
        private readonly IVelocityFileGenerator fileGenerator;

        /// <summary>
        /// </summary>
        private readonly IDbScriptFolderConfigurationSetting folderSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vs2008SolutionGenerator"/> class.
        /// </summary>
        /// <param name="databaseSettings">
        /// The databaseSettings.
        /// </param>
        /// <param name="folderSettings">
        /// The folder Settings.
        /// </param>
        /// <param name="fileGenerator">
        /// The file Generator.
        /// </param>
        public Vs2008SolutionGenerator(
                IMsSqlConnectionSettings databaseSettings,
                IDbScriptFolderConfigurationSetting folderSettings,
                IVelocityFileGenerator fileGenerator)
        {
            this.databaseSettings = databaseSettings;
            this.fileGenerator = fileGenerator;
            this.folderSettings = folderSettings;
        }

        /// <summary>
        /// Gets BaselinePath.
        /// </summary>
        /// <value>
        /// The baseline path.
        /// </value>
        private string BaselinePath
        {
            get
            {
                string buildPath = Path.Combine(OutputPath, "build");
                return Path.Combine(buildPath, "baseline");
            }
        }

        /// <summary>
        /// Gets SandboxDatabaseName.
        /// </summary>
        /// <value>
        /// The sandbox database name.
        /// </value>
        private string SandboxDatabaseName
        {
            get { return databaseSettings.DatabaseName + "Sandbox"; }
        }

        /// <summary>
        /// Gets BuildPath.
        /// </summary>
        /// <value>
        /// The build path.
        /// </value>
        private string BuildPath
        {
            get { return Path.Combine(OutputPath, "build"); }
        }

        /// <summary>
        /// Gets OutputPath.
        /// </summary>
        /// <value>
        /// The output path.
        /// </value>
        private string OutputPath
        {
            get { return folderSettings.OutputFolder; }
        }

        /// <summary>
        /// Gets SandboxPath.
        /// </summary>
        /// <value>
        /// The sandbox path.
        /// </value>
        private string SandboxPath
        {
            get { return Path.Combine(BuildPath, "sandbox"); }
        }

        #region IDbSolutionGenerator Members

        /// <summary>
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Generate(Action<string> message)
        {
            MoveDbScriptsToFinalFolders();

            CreateSolutionFile();
            CreateDbProjectFile();

            CreateBatchFiles();

            CreateNantBuildFiles();

            CreateSqlSandboxFiles();

            CreateDeveloperProperties();

            CopyFolderSkeleton();
        }

        #endregion

        /// <summary>
        /// </summary>
        private void CreateDeveloperProperties()
        {
            Directory.CreateDirectory(OutputPath);

            Hashtable velocityContext = new Hashtable();
            velocityContext["dbfriend-projectName"] = databaseSettings.DatabaseName;

            GenerateFile(velocityContext, "developer.properties.xml.vm", Path.Combine(BuildPath, "developer.properties.xml"));
        }

        /// <summary>
        /// </summary>
        private void CopyFolderSkeleton()
        {
            IDirectoryCopier copier = new DirectoryCopier();
            string resources = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
            string skeleton = Path.Combine(resources, "skeleton");

            copier.Copy(skeleton, OutputPath);
        }

        /// <summary>
        /// </summary>
        private void MoveDbScriptsToFinalFolders()
        {
            Directory.CreateDirectory(BuildPath);
            MakeSureTargetDirectoryIsMissing(BaselinePath);

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(OutputPath, "DbScripts"));
            directoryInfo.MoveTo(BaselinePath);
        }

        /// <summary>
        /// </summary>
        /// <param name="targetPath">
        /// The target path.
        /// </param>
        private void MakeSureTargetDirectoryIsMissing(string targetPath)
        {
            if (Directory.Exists(targetPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(targetPath);
                directoryInfo.Delete(true);
            }
        }

        /// <summary>
        /// </summary>
        private void CreateDbProjectFile()
        {
            Directory.CreateDirectory(OutputPath);

            Hashtable velocityContext = new Hashtable();

            string dbrefString = string.Format("{0}.{1}.dbo", "localhost", SandboxDatabaseName);
            velocityContext["dbref"] = dbrefString;
            velocityContext["dbconnstring"] = string.Format("Data Source=localhost;Initial Catalog={0};Integrated Security=True;", SandboxDatabaseName);
            velocityContext["spList"] = GetScriptedStoredProcedureList();
            velocityContext["tableList"] = GetScriptedTableList();
            velocityContext["functionList"] = GetScriptedFunctionList();
            velocityContext["viewList"] = GetScriptedViewList();
            velocityContext["projectName"] = databaseSettings.DatabaseName;

            GenerateFile(velocityContext, Path.Combine("VS2008", "stub.dbp.vm"), Path.Combine(OutputPath, databaseSettings.DatabaseName + "DB.dbp"));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedView> GetScriptedViewList()
        {
            string baselinePath = BaselinePath;

            List<IScriptedView> list = new List<IScriptedView>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "Views")))
            {
                list.Add(new ScriptedView(GetBaseFileName(fileName)));
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedFunction> GetScriptedFunctionList()
        {
            string baselinePath = BaselinePath;

            List<IScriptedFunction> list = new List<IScriptedFunction>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "UDFs")))
            {
                list.Add(new ScriptedFunction(GetBaseFileName(fileName)));
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedTable> GetScriptedTableList()
        {
            string baselinePath = BaselinePath;

            List<IScriptedTable> list = new List<IScriptedTable>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "Tables")))
            {
                list.Add(new ScriptedTable(GetBaseFileName(fileName)));
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedStoredProcedure> GetScriptedStoredProcedureList()
        {
            string baselinePath = BaselinePath;

            List<IScriptedStoredProcedure> list = new List<IScriptedStoredProcedure>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "SPs")))
            {
                list.Add(new ScriptedStoredProcedure(GetBaseFileName(fileName)));
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// </returns>
        private string GetBaseFileName(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Name;
        }

        /// <summary>
        /// </summary>
        private void CreateSolutionFile()
        {
            Directory.CreateDirectory(OutputPath);

            Hashtable velocityContext = new Hashtable();
            velocityContext["dbGuid"] = Guid.NewGuid().ToString().ToUpper();
            velocityContext["projectName"] = databaseSettings.DatabaseName;

            GenerateFile(velocityContext, Path.Combine("VS2008", "stub.sln.vm"), Path.Combine(OutputPath, databaseSettings.DatabaseName + "DB.sln"));
        }

        /// <summary>
        /// </summary>
        private void CreateBatchFiles()
        {
            Hashtable velocityContext = new Hashtable();
            velocityContext["projectName"] = databaseSettings.DatabaseName;

            GenerateFile(velocityContext, "build.bat.vm", Path.Combine(BuildPath, "build.bat"));
            GenerateFile(velocityContext, "setupdb.bat.vm", Path.Combine(BuildPath, "setupdb.bat"));
            GenerateFile(velocityContext, "sql.bat.vm", Path.Combine(BuildPath, "sql.bat"));
            GenerateFile(velocityContext, "explore.bat.vm", Path.Combine(BuildPath, "explore.bat"));

            GenerateFile(velocityContext, Path.Combine("VS2008", "open.bat.vm"), Path.Combine(BuildPath, "open.bat"));
        }

        /// <summary>
        /// </summary>
        private void CreateNantBuildFiles()
        {
            Hashtable velocityContext = new Hashtable();
            velocityContext["projectName"] = databaseSettings.DatabaseName;
            velocityContext["dbfriend-nantGetCurrentDirectory"] = "${directory::get-current-directory()}";
            velocityContext["dbfriend-nantGetBaseDir"] = "${directory::get-parent-directory(project::get-base-directory())}";


            GenerateFile(velocityContext, "stub.build.vm", Path.Combine(BuildPath, databaseSettings.DatabaseName + ".build"));
            GenerateFile(velocityContext, "stub.core.build.vm", Path.Combine(BuildPath, databaseSettings.DatabaseName + ".core.build"));
        }

        /// <summary>
        /// </summary>
        private void CreateSqlSandboxFiles()
        {
            Directory.CreateDirectory(SandboxPath);

            Hashtable velocityContext = new Hashtable();
            velocityContext["projectName"] = databaseSettings.DatabaseName;

            GenerateFile(velocityContext, "01_CreateSandbox.sql.vm", Path.Combine(SandboxPath, "01_CreateSandbox.sql"));
            GenerateFile(velocityContext, "02_SetupExternalDependencies.sql.vm", Path.Combine(SandboxPath, "02_SetupExternalDependencies.sql"));
            GenerateFile(velocityContext, "03_DefineSynonyms.sql.vm", Path.Combine(SandboxPath, "03_DefineSynonyms.sql"));
            GenerateFile(velocityContext, "04_BuildShell.sql.vm", Path.Combine(SandboxPath, "04_BuildShell.sql"));
        }

        /// <summary>
        /// </summary>
        /// <param name="velocityContext">
        /// The velocity context.
        /// </param>
        /// <param name="templatePath">
        /// The template path.
        /// </param>
        /// <param name="outputFile">
        /// The output file.
        /// </param>
        private void GenerateFile(Hashtable velocityContext, string templatePath, string outputFile)
        {
            fileGenerator.Generate(templatePath, outputFile, velocityContext);
        }
    }
}