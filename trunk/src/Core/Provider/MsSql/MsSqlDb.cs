using System.Collections.Generic;

namespace DbFriend.Core.Provider.MsSql
{
    public class MsSqlDb : IMsSqlDb
    {
        private IMsSqlDbUserObjectRepository userObjectRepository;

        public MsSqlDb(IMsSqlDbUserObjectRepository userObjectRepository)
        {
            this.userObjectRepository = userObjectRepository;
        }

        #region IMsSqlDb Members

        public List<IMsSqlObject> UserObjects
        {
            get { return new List<IMsSqlObject>(userObjectRepository.GetUserObjects()); }
        }

        #endregion
    }
}