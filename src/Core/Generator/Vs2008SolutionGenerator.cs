// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Vs2008SolutionGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Vs2008SolutionGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Settings;
    using Graph;
    using Provider;
    using Provider.MsSql;
    using NVelocity;

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
        /// </summary>
        private IDependencyGenerator dependencyGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vs2008SolutionGenerator"/> class.
        /// </summary>
        /// <param name="databaseSettings">
        /// The database settings.
        /// </param>
        /// <param name="folderSettings">
        /// The folder settings.
        /// </param>
        /// <param name="fileGenerator">
        /// The file generator.
        /// </param>
        /// <param name="dependencyGenerator">
        /// The dependency generator.
        /// </param>
        public Vs2008SolutionGenerator(
            IMsSqlConnectionSettings databaseSettings,
            IDbScriptFolderConfigurationSetting folderSettings,
            IVelocityFileGenerator fileGenerator,
            IDependencyGenerator dependencyGenerator)
        {
            this.databaseSettings = databaseSettings;
            this.fileGenerator = fileGenerator;
            this.dependencyGenerator = dependencyGenerator;
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
        /// Gets SandboxPath.
        /// </summary>
        /// <value>
        /// The sandbox path.
        /// </value>
        private string SandboxPath
        {
            get { return Path.Combine(BuildPath, "sandbox"); }
        }

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
        private void CreateBatchFiles()
        {
            var velocityContext = new VelocityContext();
            velocityContext.Put("projectName", databaseSettings.DatabaseName);

            GenerateFile(velocityContext, "build.bat.vm", Path.Combine(BuildPath, "build.bat"));
            GenerateFile(velocityContext, "setupdb.bat.vm", Path.Combine(BuildPath, "setupdb.bat"));
            GenerateFile(velocityContext, "sql.bat.vm", Path.Combine(BuildPath, "sql.bat"));
            GenerateFile(velocityContext, "explore.bat.vm", Path.Combine(BuildPath, "explore.bat"));

            GenerateFile(velocityContext, Path.Combine("VS2008", "open.bat.vm"), Path.Combine(BuildPath, "open.bat"));
        }

        /// <summary>
        /// </summary>
        private void CreateDbProjectFile()
        {
            Directory.CreateDirectory(OutputPath);

            var velocityContext = new VelocityContext();

            string dbrefString = string.Format("{0}.{1}.dbo", "localhost", SandboxDatabaseName);
            velocityContext.Put("dbref", dbrefString);
            velocityContext.Put("dbconnstring", string.Format(
                                                    "Data Source=localhost;Initial Catalog={0};Integrated Security=True;",
                                                    SandboxDatabaseName));
            velocityContext.Put("spList", GetScriptedStoredProcedureList());
            velocityContext.Put("tableList", GetScriptedTableList());
            velocityContext.Put("functionList", GetScriptedFunctionList());
            velocityContext.Put("viewList", GetScriptedViewList());
            velocityContext.Put("projectName", databaseSettings.DatabaseName);

            GenerateFile(
                velocityContext,
                Path.Combine("VS2008", "stub.dbp.vm"),
                Path.Combine(OutputPath, databaseSettings.DatabaseName + "DB.dbp"));
        }

        /// <summary>
        /// </summary>
        private void CreateDeveloperProperties()
        {
            Directory.CreateDirectory(OutputPath);

            var velocityContext = new VelocityContext();
            velocityContext.Put("dbfriend-projectName", databaseSettings.DatabaseName);

            GenerateFile(velocityContext, "developer.properties.xml.vm",
                         Path.Combine(BuildPath, "developer.properties.xml"));
        }

        /// <summary>
        /// </summary>
        private void CreateNantBuildFiles()
        {
            var velocityContext = new VelocityContext();
            velocityContext.Put("projectName", databaseSettings.DatabaseName);
            velocityContext.Put("dbfriend-nantGetCurrentDirectory", "${directory::get-current-directory()}");
            velocityContext.Put("dbfriend-nantGetBaseDir",
                                "${directory::get-parent-directory(project::get-base-directory())}");
            List<IDbObject> dependencyList = GetOrderedDependencyList();

            velocityContext.Put("sqlobjectlist", dependencyList);

            GenerateFile(velocityContext, "stub.build.vm",
                         Path.Combine(BuildPath, databaseSettings.DatabaseName + ".build"));
            GenerateFile(velocityContext, "stub.core.build.vm",
                         Path.Combine(BuildPath, databaseSettings.DatabaseName + ".core.build"));
        }

        /// <summary>
        /// </summary>
        private void CreateSolutionFile()
        {
            Directory.CreateDirectory(OutputPath);

            var velocityContext = new VelocityContext();
            velocityContext.Put("dbGuid", Guid.NewGuid().ToString().ToUpper());
            velocityContext.Put("projectName", databaseSettings.DatabaseName);

            GenerateFile(
                velocityContext,
                Path.Combine("VS2008", "stub.sln.vm"),
                Path.Combine(OutputPath, databaseSettings.DatabaseName + "DB.sln"));
        }

        /// <summary>
        /// </summary>
        private void CreateSqlSandboxFiles()
        {
            Directory.CreateDirectory(SandboxPath);

            var velocityContext = new VelocityContext();
            velocityContext.Put("projectName", databaseSettings.DatabaseName);

            GenerateFile(velocityContext, "01_CreateSandbox.sql.vm", Path.Combine(SandboxPath, "01_CreateSandbox.sql"));
            GenerateFile(
                velocityContext, "02_SetupExternalDependencies.sql.vm",
                Path.Combine(SandboxPath, "02_SetupExternalDependencies.sql"));
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
        private void GenerateFile(VelocityContext velocityContext, string templatePath, string outputFile)
        {
            fileGenerator.Generate(templatePath, outputFile, velocityContext);
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
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Name;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IDbObject> GetOrderedDependencyList()
        {
            IDependencyGraph graph = dependencyGenerator.GenerateGraph();
            return new List<IDbObject>(graph.Dependencies);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedFunction> GetScriptedFunctionList()
        {
            string baselinePath = BaselinePath;

            var list = new List<IScriptedFunction>();
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
        private List<IScriptedStoredProcedure> GetScriptedStoredProcedureList()
        {
            string baselinePath = BaselinePath;

            var list = new List<IScriptedStoredProcedure>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "SPs")))
            {
                list.Add(new ScriptedStoredProcedure(GetBaseFileName(fileName)));
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

            var list = new List<IScriptedTable>();
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
        private List<IScriptedView> GetScriptedViewList()
        {
            string baselinePath = BaselinePath;

            var list = new List<IScriptedView>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "Views")))
            {
                list.Add(new ScriptedView(GetBaseFileName(fileName)));
            }

            return list;
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
                var directoryInfo = new DirectoryInfo(targetPath);
                directoryInfo.Delete(true);
            }
        }

        /// <summary>
        /// </summary>
        private void MoveDbScriptsToFinalFolders()
        {
            Directory.CreateDirectory(BuildPath);
            MakeSureTargetDirectoryIsMissing(BaselinePath);

            var directoryInfo = new DirectoryInfo(Path.Combine(OutputPath, "DbScripts"));
            directoryInfo.MoveTo(BaselinePath);
        }
    }
}