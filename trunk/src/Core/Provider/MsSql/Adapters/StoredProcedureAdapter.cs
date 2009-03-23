using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public class StoredProcedureAdapter : IStoredProcedureAdapter
    {
        private readonly StoredProcedure procedure;

        public StoredProcedureAdapter(StoredProcedure procedure)
        {
            this.procedure = procedure;
        }

        #region IStoredProcedureAdapter Members

        public string Urn
        {
            get { return procedure.Urn; }
        }

        public bool IsSystemObject
        {
            get { return procedure.IsSystemObject; }
        }

        public string Name
        {
            get { return procedure.Name; }
        }

        public string Owner
        {
            get { return procedure.Owner; }
        }

        public StringCollection Script(ScriptingOptions options)
        {
            return procedure.Script(options);
        }

        #endregion
    }
}