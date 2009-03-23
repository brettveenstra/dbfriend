using System;
using System.Collections.Generic;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Core.Provider.MsSql.Adapters;
using DbFriend.Testing.Extensions;
using DbFriend.Testing.Utility;
using MbUnit.Framework;
using Rhino.Mocks;

namespace DbFriend.Testing.Unit.Provider.MsSql
{
    public class MsSqlDependencyRepositoryTest : Specification<MsSqlDependencyRepository>
    {

        [Test]
        public void Should_Take_SqlSmoAdapterObject_And_Produce_FirstLevelDependencies()
        {
            IDependencyWalkerAdapter mockWalker = MockingContext.Get<IDependencyWalkerAdapter>();
            IDependencyTreeNodeAdapterMsSqlObjectMapper mockMapper =
                MockingContext.Get<IDependencyTreeNodeAdapterMsSqlObjectMapper>();

            IMsSqlObject stubSqlObject = MockRepository.GenerateStub<IMsSqlObject>();

            mockWalker.Expect(x => x.DiscoveredDependencies(stubSqlObject))
                .Return(StubbedTreeNodeDependencies);

            mockMapper.Expect(x => x.MapFrom(null))
                .IgnoreArguments()
                .Repeat.Times(StubbedTreeNodeDependencies.Count)
                .Return(StubbedMsSqlObject);

            List<IMsSqlObject> list = new List<IMsSqlObject>(Sut.GetDependencies(stubSqlObject));
            
            list.Count.ShouldBe(StubbedTreeNodeDependencies.Count);

            stubSqlObject.VerifyAllExpectations();
            mockMapper.VerifyAllExpectations();
            mockWalker.VerifyAllExpectations();

        }

        protected IMsSqlObject StubbedMsSqlObject
        {
            get { return MockRepository.GenerateStub<IMsSqlObject>(); }
        }

        protected List<IDependencyTreeNodeAdapter> StubbedTreeNodeDependencies
        {
            get
            {
                List<IDependencyTreeNodeAdapter> list = new List<IDependencyTreeNodeAdapter>();
                list.Add(MockRepository.GenerateStub<IDependencyTreeNodeAdapter>());
                list.Add(MockRepository.GenerateStub<IDependencyTreeNodeAdapter>());
                list.Add(MockRepository.GenerateStub<IDependencyTreeNodeAdapter>());
                return list;
            }
        }

        protected override void Before_Each_Spec()
        {
        }
    }
}