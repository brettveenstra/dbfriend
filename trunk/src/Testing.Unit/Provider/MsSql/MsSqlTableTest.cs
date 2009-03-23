using System.Collections.Generic;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Testing.Extensions;
using DbFriend.Testing.Utility;
using MbUnit.Framework;
using Rhino.Mocks;

namespace DbFriend.Testing.Unit.Provider.MsSql
{
    public class MsSqlTableTest : Specification<MsSqlTable>
    {
        protected IEnumerable<IMsSqlObject> StubEnumerableMsSqlObjects
        {
            get
            {
                yield return MockRepository.GenerateStub<IMsSqlObject>();
                yield return MockRepository.GenerateStub<IMsSqlObject>();
            }
        }

        [Test]
        public void Can_Provide_Dependencies()
        {
            IMsSqlDependencyRepository dependencyRepository = MockingContext.Get<IMsSqlDependencyRepository>();

            dependencyRepository.Expect(x => x.GetDependencies(Sut))
                .Return(StubEnumerableMsSqlObjects);
            
            List<IMsSqlObject> list = new List<IMsSqlObject>(Sut.Dependencies);

            dependencyRepository.VerifyAllExpectations();

            list.Count.ShouldBe(2);
        }

        protected override void Before_Each_Spec()
        {
        }
    }
}