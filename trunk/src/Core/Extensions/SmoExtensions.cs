// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="SmoExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SmoExtensions type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Extensions
{
    /// <summary>
    /// </summary>
    public static class SmoExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="database">
        /// The database.
        /// </param>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <returns>
        /// </returns>
        public static Table AddTable(this Database database, string tableName)
        {
            database.Tables.Add(new Table(database, tableName, "dbo"));
            return database.Tables[tableName];
        }

        /// <summary>
        /// </summary>
        /// <param name="table">
        /// The table.
        /// </param>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <returns>
        /// </returns>
        public static Table AddPrimaryIdentityColumn(this Table table, string columnName)
        {
            Column column = table.BuildColumn(columnName, DataType.Int);
            column.Identity = true;
            column.IdentityIncrement = 1;

            table.Columns.Add(column);
            return table;
        }

        /// <summary>
        /// </summary>
        /// <param name="table">
        /// The table.
        /// </param>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <param name="dataType">
        /// The data type.
        /// </param>
        /// <returns>
        /// </returns>
        public static Table AddColumn(this Table table, string columnName, DataType dataType)
        {
            table.Columns.Add(table.BuildColumn(columnName, dataType));
            return table;
        }

        /// <summary>
        /// </summary>
        /// <param name="table">
        /// The table.
        /// </param>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <param name="dataType">
        /// The data type.
        /// </param>
        /// <returns>
        /// </returns>
        public static Column BuildColumn(this Table table, string columnName, DataType dataType)
        {
            Column column = new Column(table, columnName, dataType);
            return column;
        }

        public static void Commit(this Table table)
        {
            table.Create();
        }
    }
}