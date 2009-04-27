using System.Collections.Generic;

namespace DbFriend.Core.Provider.MsSql
{
    public interface IMsSqlDbUserObjectRepository
    {
        IEnumerable<IMsSqlObject> GetUserObjects();
    }
}