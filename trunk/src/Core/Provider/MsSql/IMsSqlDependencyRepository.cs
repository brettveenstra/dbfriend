using System.Collections.Generic;

namespace DbFriend.Core.Provider.MsSql
{
    public interface IMsSqlDependencyRepository
    {
        IEnumerable<IMsSqlObject> GetDependencies(IMsSqlObject msSqlObject);
    }
}