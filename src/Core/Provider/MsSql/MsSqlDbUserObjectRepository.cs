using System.Collections.Generic;
using DbFriend.Core.Provider.MsSql.Adapters;

namespace DbFriend.Core.Provider.MsSql
{
    public class MsSqlDbUserObjectRepository : IMsSqlDbUserObjectRepository
    {
        private IMsSqlDatabaseConnectionAdapter databaseConnectionAdapter;

        public MsSqlDbUserObjectRepository(IMsSqlDatabaseConnectionAdapter databaseConnectionAdapter)
        {
            this.databaseConnectionAdapter = databaseConnectionAdapter;
        }

        #region IMsSqlDbUserObjectRepository Members

        public IEnumerable<IMsSqlObject> GetUserObjects()
        {
            databaseConnectionAdapter.Connect();

            foreach (IMsSqlObject sqlObject in databaseConnectionAdapter.Tables)
            {
                yield return sqlObject;
            }

            foreach (IMsSqlObject sqlObject in databaseConnectionAdapter.StoredProcedures)
            {
                yield return sqlObject;
            }

            foreach (IMsSqlObject sqlObject in databaseConnectionAdapter.Views)
            {
                yield return sqlObject;
            }

            foreach (IMsSqlObject sqlObject in databaseConnectionAdapter.Functions)
            {
                yield return sqlObject;
            }

            databaseConnectionAdapter.Disconnect();
        }

        #endregion
    }
}