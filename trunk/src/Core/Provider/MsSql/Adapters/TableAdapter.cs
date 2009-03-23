using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public class TableAdapter : ITableAdapter
    {
        private readonly Table table;

        public TableAdapter(Table table)
        {
            this.table = table;
        }

        #region ITableAdapter Members

        public string Urn
        {
            get { return table.Urn; }
        }

        public string Name
        {
            get { return table.Name; }
        }

        public string Owner
        {
            get { return table.Owner; }
        }

        public bool IsSystemObject
        {
            get { return table.IsSystemObject; }
        }

        public StringCollection Script(ScriptingOptions options)
        {
            return table.Script(options);
        }

        #endregion
    }
}