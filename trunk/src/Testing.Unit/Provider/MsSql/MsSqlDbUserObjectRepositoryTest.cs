using System.Collections.Generic;
using DbFriend.Core.Provider;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Core.Provider.MsSql.Adapters;
using DbFriend.Testing.Unit.Utility;
using MbUnit.Framework;
using Rhino.Mocks;

namespace DbFriend.Testing.Unit.Provider.MsSql
{
    public class MsSqlDbUserObjectRepositoryTest : Specification<MsSqlDbUserObjectRepository>
    {
        private IEnumerable<IMsSqlObject> StubbedSqlObjects
        {
            get { yield return MockRepository.GenerateStub<IMsSqlObject>(); }
        }

        [Test]
        public void RepositoryProvidesUserObjects()
        {
            IMsSqlDatabaseConnectionAdapter mockConnection = MockingContext.Get<IMsSqlDatabaseConnectionAdapter>();
            mockConnection.Expect(x => x.Connect())
                    .Repeat.Once();
            mockConnection.Expect(x => x.StoredProcedures)
                    .Return(StubbedSqlObjects)
                    .Repeat.Once();
            mockConnection.Expect(x => x.Tables)
                    .Return(StubbedSqlObjects)
                    .Repeat.Once();
            mockConnection.Expect(x => x.Views)
                    .Return(StubbedSqlObjects)
                    .Repeat.Once();
            mockConnection.Expect(x => x.Functions)
                    .Return(StubbedSqlObjects)
                    .Repeat.Once();
            mockConnection.Expect(x => x.Disconnect())
                    .Repeat.Once();

            Assert.IsNotNull(new List<IMsSqlObject>(Sut.GetUserObjects()));
            mockConnection.VerifyAllExpectations();
        }

        protected override void Before_Each_Spec()
        {
        }
    }
}