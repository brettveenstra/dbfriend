using System.Collections.Generic;

namespace DbFriend.Core.Provider.MsSql
{
    public interface IMsSqlDb
    {
        List<IMsSqlObject> UserObjects { get; }
    }
}