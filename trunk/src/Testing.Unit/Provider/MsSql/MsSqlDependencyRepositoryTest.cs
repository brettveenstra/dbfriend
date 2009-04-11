// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDependencyRepositoryTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDependencyRepositoryTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Testing.Unit.Provider.MsSql
{
    using System.Collections.Generic;

    using DbFriend.Core.Provider.MsSql;
    using DbFriend.Core.Provider.MsSql.Adapters;
    using DbFriend.Testing.Extensions;

    using MbUnit.Framework;

    using Rhino.Mocks;

    using Utility;

    /// <summary>
    /// </summary>
    public class MsSqlDependencyRepositoryTest : Specification<MsSqlDependencyRepository>
    {
        /// <summary>
        /// Gets StubbedMsSqlObject.
        /// </summary>
        /// <value>
        /// The stubbed ms sql object.
        /// </value>
        protected IMsSqlObject StubbedMsSqlObject
        {
            get
            {
                return MockRepository.GenerateStub<IMsSqlObject>();
            }
        }

        /// <summary>
        /// Gets StubbedTreeNodeDependencies.
        /// </summary>
        /// <value>
        /// The stubbed tree node dependencies.
        /// </value>
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

        /// <summary>
        /// </summary>
        [Test]
        public void Should_Take_SqlSmoAdapterObject_And_Produce_FirstLevelDependencies()
        {
            IDependencyWalkerAdapter mockWalker = this.MockingContext.Get<IDependencyWalkerAdapter>();
            IDependencyTreeNodeAdapterMsSqlObjectMapper mockMapper = this.MockingContext.Get<IDependencyTreeNodeAdapterMsSqlObjectMapper>();

            IMsSqlObject stubSqlObject = MockRepository.GenerateStub<IMsSqlObject>();

            mockWalker.Expect(x => x.DiscoveredDependencies(stubSqlObject)).Return(this.StubbedTreeNodeDependencies);

            mockMapper.Expect(x => x.MapFrom(null)).IgnoreArguments().Repeat.Times(this.StubbedTreeNodeDependencies.Count).Return(
                    this.StubbedMsSqlObject);

            List<IMsSqlObject> list = new List<IMsSqlObject>(this.Sut.GetDependencies(stubSqlObject));

            list.Count.ShouldBe(this.StubbedTreeNodeDependencies.Count);

            stubSqlObject.VerifyAllExpectations();
            mockMapper.VerifyAllExpectations();
            mockWalker.VerifyAllExpectations();
        }

        /// <summary>
        /// </summary>
        protected override void Before_Each_Spec()
        {
        }
    }
}