using System.Collections;
using System.Collections.Generic;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public interface IDatabaseWrapper
    {
        IEnumerable<IUserDefinedFunctionAdapter> UserDefinedFunctions { get; }
        IEnumerable<IStoredProcedureAdapter> StoredProcedures { get; }
        IEnumerable<ITableAdapter> Tables { get; }
        IEnumerable<IViewAdapter> Views { get; }
    }
}