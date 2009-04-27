using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using DbFriend.Core.Graph;
using QuickGraph;

namespace DbFriend.Core.Provider.MsSql
{
    public class MsSqlDependencyGenerator : IDependencyGenerator
    {
        private readonly IDependencyGraph graph;
        private readonly IMsSqlConnectionSettings connectionSettings;

        public MsSqlDependencyGenerator(IDependencyGraph graph, IMsSqlConnectionSettings connectionSettings)
        {
            this.graph = graph;
            this.connectionSettings = connectionSettings;
        }

        #region IDependencyGenerator Members

        public IDependencyGraph GenerateGraph()
        {
            using (SqlConnection sqlconnection = new SqlConnection(connectionSettings.AdoNetConnectionString))
            {
                sqlconnection.Open();
                AddDatabaseObjectsTo(graph, sqlconnection);
                AddDependenciesTo(graph, sqlconnection);
            }

            return graph;
        }

        private static void AddDependenciesTo(IDependencyGraph graph, SqlConnection sqlConnection)
        {
            WireForeignKeyDependenciesData(sqlConnection, graph);
            WireSql_DependenciesData(sqlConnection, graph);

            WireDeepDependencies(graph, sqlConnection, GetQuerableObjects(sqlConnection),
                                 GetExecutableObjects(sqlConnection));
        }

        private static void AddDatabaseObjectsTo(IDependencyGraph graph, SqlConnection sqlConnection)
        {
            var command =
                new SqlCommand(
                    "SELECT o.[name], o.[id], o.[xtype], u.[name] as [schema] FROM sysobjects o INNER JOIN sysusers u ON u.uid = o.uid WHERE xtype NOT IN ( 's', 'pk', 'tr', 'fk', 'fn', 'sq', 'it', 'f', 'd' )",
                    sqlConnection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader != null)
            {
                var dataTable = new DataTable();
                dataTable.Load(sqlDataReader);

                foreach (DataRow row in dataTable.Rows)
                {
                    graph.AddDbObject(CreateDbObjectFrom((int) row["id"], row["name"].ToString(),
                                                         row["xtype"].ToString(), row["schema"].ToString()));
                }
            }
        }

        #endregion

        private static IDbObject CreateDbObjectFrom(int id, string name, string type, string schema)
        {
            return new DbObject(id, name.Trim(), type.Trim(), schema);
        }

        private static DbObject CreateDbObjectFrom(DataRow row)
        {
            var id = (int) row["id"];
            string name = row["name"].ToString();
            string type = row["xtype"].ToString();
            string schema = row["schema"].ToString();

            return new DbObject(id, name, type, schema);
        }

        private static void WireForeignKeyDependenciesData(SqlConnection sqlConnection, IDependencyGraph graph)
        {
            var fkeyCommand = new SqlCommand(
                "SELECT DISTINCT o2.[id] AS ChildId, o2.[name] AS ChildName, o2.[xtype] AS ChildType, childu.[name] AS ChildSchema, " +
                "o3.[id] AS ParentId, o3.[name] AS ParentName, o3.[xtype] AS ParentType, parentu.[name] AS ParentSchema " +
                "FROM sys.foreign_keys fk INNER JOIN sysobjects o1 ON o1.[id] = fk.[object_id] " +
                "INNER JOIN sysobjects o2 ON o2.[id] = fk.[parent_object_id] " +
                "INNER JOIN sysobjects o3 ON o3.[id] = fk.[referenced_object_id]" +
                "INNER JOIN sys.sysusers childu ON childu.uid = o2.uid " +
                "INNER JOIN sys.sysusers parentu ON parentu.uid = o3.uid " +
                "WHERE o3.[id] <> o2.[id]",
                sqlConnection);
            SqlDataReader fkeyDataReader = fkeyCommand.ExecuteReader();
            if (fkeyDataReader != null)
            {
                var dataTable = new DataTable();
                dataTable.Load(fkeyDataReader);

                foreach (DataRow row in dataTable.Rows)
                {
                    IDbObject child = CreateDbObjectFrom((int) row["childid"],
                                                         row["childname"].ToString(),
                                                         row["childtype"].ToString(),
                                                         row["childschema"].ToString());
                    IDbObject parent = CreateDbObjectFrom((int) row["parentid"],
                                                          row["parentname"].ToString(),
                                                          row["parenttype"].ToString(),
                                                          row["parentschema"].ToString());

                    graph.AddDependency(child, parent);
                }
            }
        }

        private static void WireSql_DependenciesData(SqlConnection sqlConnection, IDependencyGraph graph)
        {
            var sysdepCommand =
                new SqlCommand(
                    "SELECT DISTINCT child.[id] AS ChildId, child.[name] AS ChildName, child.[xtype] AS ChildType, childu.[name] AS ChildSchema, " +
                    "parent.[id] AS ParentId, parent.[name] AS ParentName, parent.[xtype] AS ParentType, parentu.[name] AS ParentSchema " +
                    "FROM sys.sql_dependencies dep INNER JOIN sysobjects child ON child.id = dep.[object_id] " +
                    "INNER JOIN sys.sysobjects parent ON parent.id = dep.[referenced_major_id] " +
                    "INNER JOIN sys.sysusers childu ON childu.uid = child.uid " +
                    "INNER JOIN sys.sysusers parentu ON parentu.uid = parent.uid " +
                    "WHERE child.[xtype] NOT IN ( 's', 'pk', 'tr', 'fk', 'fn', 'sq', 'it' )",
                    sqlConnection);
            SqlDataReader sysdepDataReader = sysdepCommand.ExecuteReader();
            if (sysdepDataReader != null)
            {
                var dataTable = new DataTable();
                dataTable.Load(sysdepDataReader);

                foreach (DataRow row in dataTable.Rows)
                {
                    IDbObject child = CreateDbObjectFrom((int) row["childid"],
                                                         row["childname"].ToString(),
                                                         row["childtype"].ToString(),
                                                         row["childschema"].ToString());
                    IDbObject parent = CreateDbObjectFrom((int) row["parentid"],
                                                          row["parentname"].ToString(),
                                                          row["parenttype"].ToString(),
                                                          row["parentschema"].ToString());

                    var edge = new Edge<IDbObject>(child, parent);
                    graph.AddDependency(child, parent);
                }
            }
        }

        private static List<IDbObject> GetQuerableObjects(SqlConnection sqlConnection)
        {
            var querableObjects = new List<IDbObject>();

            var querableObjectsCommand = new SqlCommand("SELECT u.[name] AS [Schema], o.[name], o.[id], o.[xtype] "
                                                        +
                                                        "FROM sysobjects o INNER JOIN sysusers u ON u.uid = o.uid WHERE xtype IN ( 'u', 'v', 'if' )",
                                                        sqlConnection);
            SqlDataReader querableDataReader = querableObjectsCommand.ExecuteReader();
            if (querableDataReader != null)
            {
                var dataTable = new DataTable();
                dataTable.Load(querableDataReader);
                foreach (DataRow row in dataTable.Rows)
                {
                    IDbObject dbObject = CreateDbObjectFrom((int) row["id"], row["name"].ToString(),
                                                            row["xtype"].ToString(), row["schema"].ToString());
                    querableObjects.Add(dbObject);
                }
            }
            return querableObjects;
        }

        private static List<IDbObject> GetExecutableObjects(SqlConnection sqlConnection)
        {
            var executableObjects = new List<IDbObject>();
            var executableObjectsCommand = new SqlCommand("SELECT u.[name] AS [Schema], o.[name], o.[id], o.[xtype] "
                                                          +
                                                          "FROM sysobjects o INNER JOIN sysusers u ON u.uid = o.uid WHERE xtype IN ( 'p' )",
                                                          sqlConnection);
            SqlDataReader executableObjectsDataReader = executableObjectsCommand.ExecuteReader();
            if (executableObjectsDataReader != null)
            {
                var dataTable = new DataTable();
                dataTable.Load(executableObjectsDataReader);
                foreach (DataRow row in dataTable.Rows)
                {
                    IDbObject dbObject = CreateDbObjectFrom((int) row["id"], row["name"].ToString(),
                                                            row["xtype"].ToString(), row["schema"].ToString());
                    executableObjects.Add(dbObject);
                }
            }
            return executableObjects;
        }

        private static void WireDeepDependencies(IDependencyGraph graph,
                                                 SqlConnection sqlConnection,
                                                 List<IDbObject> querableObjects,
                                                 List<IDbObject> executableObjects)
        {
            var dbObjects = new List<IDbObject>(graph.DbObjects);

            foreach (IDbObject child in dbObjects)
            {
                var sqlCommand = new SqlCommand("SELECT definition FROM sys.sql_modules WHERE [object_id] = @objid",
                                                sqlConnection);
                sqlCommand.Parameters.AddWithValue("@objid", child.Id);

                var definitionText = sqlCommand.ExecuteScalar() as string;
                if (definitionText != null)
                {
                    Debug.WriteLine(string.Format("=== Analyzing: {0}", child.Name));
                    foreach (IDbObject parent in GetTargets(definitionText, querableObjects, executableObjects))
                    {
                        graph.AddDependency(child, parent);
                    }
                }
            }
        }

        private static IEnumerable<IDbObject> GetTargets(string definitionText, List<IDbObject> querableObjects,
                                                         List<IDbObject> executableObjects)
        {
            var lastChar = new char();
            bool inlineComment = false;
            bool blockComment = false;

            string[] lines = definitionText.Split('\n');
            foreach (string line in lines)
            {
                string word = string.Empty;
                bool expectingQuerableTarget = false;

                if (line.Length == 0)
                {
                    continue;
                }

                if (blockComment)
                {
                    if (line.IndexOf("*/") == 0)
                    {
                        blockComment = false;
                        continue;
                    }
                }

                if (line.StartsWith("--"))
                {
                    continue;
                }

                if (word.Length > 0)
                {
                    IDbObject executableObject = GetDbObjectFrom(executableObjects, word);
                    if (executableObject != null)
                    {
                        yield return executableObject;
                        continue;
                    }
                }

                if (inlineComment)
                {
                    inlineComment = false;
                    continue;
                }

                bool expectingExecutableTarget = false;
                for (int i = 0; i < line.Length; i++)
                {
                    char currentChar = line[i];

                    switch (currentChar)
                    {
                        case '-':
                            if (lastChar == '-')
                            {
                                inlineComment = true;
                            }
                            break;

                        case '*':
                            if (lastChar == '/')
                            {
                                blockComment = true;
                            }
                            break;

                        case '/':
                            if (lastChar == '*')
                            {
                                blockComment = false;
                            }
                            break;

                        case '\n':
                            inlineComment = false;
                            break;

                        default:

                            if (char.IsWhiteSpace(currentChar)) //end of word
                            {
                                switch (word.ToLower())
                                {
                                    case "from":
                                    case "join":
                                        expectingQuerableTarget = true;
                                        word = string.Empty;
                                        continue;
                                    case "exec":
                                    case "execute":
                                        word = string.Empty;
                                        expectingExecutableTarget = true;
                                        continue;
                                    default:
                                        break;
                                }

                                if (expectingQuerableTarget && word.Length > 0)
                                {
                                    IDbObject queryable = GetDbObjectFrom(querableObjects, word);
                                    if (queryable != null)
                                    {
                                        expectingQuerableTarget = false;
                                        yield return queryable;
                                    }
                                }

                                if (expectingExecutableTarget)
                                {
                                    IDbObject expectedExecutable = GetDbObjectFrom(executableObjects, word);
                                    if (expectedExecutable != null)
                                    {
                                        expectingExecutableTarget = false;
                                        yield return expectedExecutable;
                                        continue;
                                    }
                                }

                                word = string.Empty;
                            }
                            else
                            {
                                word += currentChar;
                            }
                            break;
                    }
                    lastChar = currentChar;
                }
            }
        }

        private static IDbObject GetDbObjectFrom(List<IDbObject> dbobjectList, string word)
        {
            word = word.Replace("[", string.Empty).Replace("]", string.Empty);
            return dbobjectList.Find(x => x.Name.Equals(word, StringComparison.OrdinalIgnoreCase)) ??
                   dbobjectList.Find(x => (x.Schema + "." + x.Name).Equals(word, StringComparison.OrdinalIgnoreCase));
        }
    }
}