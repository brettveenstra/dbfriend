using System.Collections.Generic;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Testing.Unit.Utility;
using MbUnit.Framework;
using Rhino.Mocks;

namespace DbFriend.Testing.Unit.Provider.MsSql
{
    public class MsSqlDbTest : Specification<MsSqlDb>
    {
        protected IEnumerable<IMsSqlObject> StubbedDatabaseUserObjects
        {
            get { yield return MockRepository.GenerateStub<IMsSqlObject>(); }
        }

        [Test]
        public void DatabaseProvidesUserObjects()
        {
            IMsSqlDbUserObjectRepository mockRepository = MockingContext.Get<IMsSqlDbUserObjectRepository>();
            mockRepository.Expect(x => x.GetUserObjects())
                    .Return(StubbedDatabaseUserObjects)
                    .Repeat.Once();

            // act
            List<IMsSqlObject> userObjects = Sut.UserObjects;

            // assert
            mockRepository.VerifyAllExpectations();
            Assert.IsNotNull(userObjects);
        }

        protected override void Before_Each_Spec()
        {
        }
    }
}