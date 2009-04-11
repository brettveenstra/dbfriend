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
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    using DbFriend.Core.Generator.Settings;
    using DbFriend.Core.Graph;
    using DbFriend.Core.Provider.MsSql;

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
        private IDependencyGraph dependencyGraph;

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
        /// <param name="dependencyGraph">
        /// </param>
        public Vs2008SolutionGenerator(
                IMsSqlConnectionSettings databaseSettings,
                IDbScriptFolderConfigurationSetting folderSettings,
                IVelocityFileGenerator fileGenerator,
                IDependencyGraph dependencyGraph)
        {
            this.databaseSettings = databaseSettings;
            this.fileGenerator = fileGenerator;
            this.dependencyGraph = dependencyGraph;
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
                string buildPath = Path.Combine(this.OutputPath, "build");
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
            get
            {
                return this.databaseSettings.DatabaseName + "Sandbox";
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
            get
            {
                return Path.Combine(this.OutputPath, "build");
            }
        }

        /// <summary>
        /// Gets OutputPath.
        /// </summary>
        /// <value>
        /// The output path.
        /// </value>
        private string OutputPath
        {
            get
            {
                return this.folderSettings.OutputFolder;
            }
        }

        /// <summary>
        /// Gets SandboxPath.
        /// </summary>
        /// <value>
        /// The sandbox path.
        /// </value>
        private string SandboxPath
        {
            get
            {
                return Path.Combine(this.BuildPath, "sandbox");
            }
        }

        #region IDbSolutionGenerator Members

        /// <summary>
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Generate(Action<string> message)
        {
            this.MoveDbScriptsToFinalFolders();

            this.CreateSolutionFile();
            this.CreateDbProjectFile();

            this.CreateBatchFiles();

            this.CreateNantBuildFiles();

            this.CreateSqlSandboxFiles();

            this.CreateDeveloperProperties();

            this.CopyFolderSkeleton();
        }

        #endregion

        /// <summary>
        /// </summary>
        private void CreateDeveloperProperties()
        {
            Directory.CreateDirectory(this.OutputPath);

            Hashtable velocityContext = new Hashtable();
            velocityContext["dbfriend-projectName"] = this.databaseSettings.DatabaseName;

            this.GenerateFile(velocityContext, "developer.properties.xml.vm", Path.Combine(this.BuildPath, "developer.properties.xml"));
        }

        /// <summary>
        /// </summary>
        private void CopyFolderSkeleton()
        {
            IDirectoryCopier copier = new DirectoryCopier();
            string resources = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
            string skeleton = Path.Combine(resources, "skeleton");

            copier.Copy(skeleton, this.OutputPath);
        }

        /// <summary>
        /// </summary>
        private void MoveDbScriptsToFinalFolders()
        {
            Directory.CreateDirectory(this.BuildPath);
            this.MakeSureTargetDirectoryIsMissing(this.BaselinePath);

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(this.OutputPath, "DbScripts"));
            directoryInfo.MoveTo(this.BaselinePath);
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
            Directory.CreateDirectory(this.OutputPath);

            Hashtable velocityContext = new Hashtable();

            string dbrefString = string.Format("{0}.{1}.dbo", "localhost", this.SandboxDatabaseName);
            velocityContext["dbref"] = dbrefString;
            velocityContext["dbconnstring"] = string.Format(
                    "Data Source=localhost;Initial Catalog={0};Integrated Security=True;", this.SandboxDatabaseName);
            velocityContext["spList"] = this.GetScriptedStoredProcedureList();
            velocityContext["tableList"] = this.GetScriptedTableList();
            velocityContext["functionList"] = this.GetScriptedFunctionList();
            velocityContext["viewList"] = this.GetScriptedViewList();
            velocityContext["projectName"] = this.databaseSettings.DatabaseName;

            this.GenerateFile(
                    velocityContext,
                    Path.Combine("VS2008", "stub.dbp.vm"),
                    Path.Combine(this.OutputPath, this.databaseSettings.DatabaseName + "DB.dbp"));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedView> GetScriptedViewList()
        {
            string baselinePath = this.BaselinePath;

            List<IScriptedView> list = new List<IScriptedView>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "Views")))
            {
                list.Add(new ScriptedView(this.GetBaseFileName(fileName)));
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedFunction> GetScriptedFunctionList()
        {
            string baselinePath = this.BaselinePath;

            List<IScriptedFunction> list = new List<IScriptedFunction>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "UDFs")))
            {
                list.Add(new ScriptedFunction(this.GetBaseFileName(fileName)));
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedTable> GetScriptedTableList()
        {
            string baselinePath = this.BaselinePath;

            List<IScriptedTable> list = new List<IScriptedTable>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "Tables")))
            {
                list.Add(new ScriptedTable(this.GetBaseFileName(fileName)));
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IScriptedStoredProcedure> GetScriptedStoredProcedureList()
        {
            string baselinePath = this.BaselinePath;

            List<IScriptedStoredProcedure> list = new List<IScriptedStoredProcedure>();
            foreach (string fileName in Directory.GetFiles(Path.Combine(baselinePath, "SPs")))
            {
                list.Add(new ScriptedStoredProcedure(this.GetBaseFileName(fileName)));
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
            Directory.CreateDirectory(this.OutputPath);

            Hashtable velocityContext = new Hashtable();
            velocityContext["dbGuid"] = Guid.NewGuid().ToString().ToUpper();
            velocityContext["projectName"] = this.databaseSettings.DatabaseName;

            this.GenerateFile(
                    velocityContext,
                    Path.Combine("VS2008", "stub.sln.vm"),
                    Path.Combine(this.OutputPath, this.databaseSettings.DatabaseName + "DB.sln"));
        }

        /// <summary>
        /// </summary>
        private void CreateBatchFiles()
        {
            Hashtable velocityContext = new Hashtable();
            velocityContext["projectName"] = this.databaseSettings.DatabaseName;

            this.GenerateFile(velocityContext, "build.bat.vm", Path.Combine(this.BuildPath, "build.bat"));
            this.GenerateFile(velocityContext, "setupdb.bat.vm", Path.Combine(this.BuildPath, "setupdb.bat"));
            this.GenerateFile(velocityContext, "sql.bat.vm", Path.Combine(this.BuildPath, "sql.bat"));
            this.GenerateFile(velocityContext, "explore.bat.vm", Path.Combine(this.BuildPath, "explore.bat"));

            this.GenerateFile(velocityContext, Path.Combine("VS2008", "open.bat.vm"), Path.Combine(this.BuildPath, "open.bat"));
        }

        /// <summary>
        /// </summary>
        private void CreateNantBuildFiles()
        {
            Hashtable velocityContext = new Hashtable();
            velocityContext["projectName"] = this.databaseSettings.DatabaseName;
            velocityContext["dbfriend-nantGetCurrentDirectory"] = "${directory::get-current-directory()}";
            velocityContext["dbfriend-nantGetBaseDir"] = "${directory::get-parent-directory(project::get-base-directory())}";
            velocityContext["sqlobjectlist"] = this.GetOrderedDependencyList();

            this.GenerateFile(velocityContext, "stub.build.vm", Path.Combine(this.BuildPath, this.databaseSettings.DatabaseName + ".build"));
            this.GenerateFile(velocityContext, "stub.core.build.vm", Path.Combine(this.BuildPath, this.databaseSettings.DatabaseName + ".core.build"));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private List<IMsSqlObject> GetOrderedDependencyList()
        {
            return new List<IMsSqlObject>(this.dependencyGraph.OrderedDependencies);
        }

        /// <summary>
        /// </summary>
        private void CreateSqlSandboxFiles()
        {
            Directory.CreateDirectory(this.SandboxPath);

            Hashtable velocityContext = new Hashtable();
            velocityContext["projectName"] = this.databaseSettings.DatabaseName;

            this.GenerateFile(velocityContext, "01_CreateSandbox.sql.vm", Path.Combine(this.SandboxPath, "01_CreateSandbox.sql"));
            this.GenerateFile(
                    velocityContext, "02_SetupExternalDependencies.sql.vm", Path.Combine(this.SandboxPath, "02_SetupExternalDependencies.sql"));
            this.GenerateFile(velocityContext, "03_DefineSynonyms.sql.vm", Path.Combine(this.SandboxPath, "03_DefineSynonyms.sql"));
            this.GenerateFile(velocityContext, "04_BuildShell.sql.vm", Path.Combine(this.SandboxPath, "04_BuildShell.sql"));
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
            this.fileGenerator.Generate(templatePath, outputFile, velocityContext);
        }
    }
}