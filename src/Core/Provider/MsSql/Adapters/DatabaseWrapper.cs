using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public class DatabaseWrapper : IDatabaseWrapper
    {
        private readonly Database database;

        public DatabaseWrapper(Database database)
        {
            this.database = database;
        }

        #region IDatabaseWrapper Members

        public IEnumerable<IUserDefinedFunctionAdapter> UserDefinedFunctions
        {
            get
            {
                foreach (UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions)
                {
                    yield return new UserDefinedFunctionAdapter(userDefinedFunction);
                }
            }
        }

        public IEnumerable<IStoredProcedureAdapter> StoredProcedures
        {
            get
            {
                foreach (StoredProcedure storedProcedure in database.StoredProcedures)
                {
                    yield return new StoredProcedureAdapter(storedProcedure);
                }
            }
        }

        public IEnumerable<ITableAdapter> Tables
        {
            get
            {
                foreach (Table table in database.Tables)
                {
                    yield return new TableAdapter(table);
                }
            }
        }

        public IEnumerable<IViewAdapter> Views
        {
            get
            {
                foreach (View view in database.Views)
                {
                    yield return new ViewAdapter(view);
                }
            }
        }

        #endregion
    }
}