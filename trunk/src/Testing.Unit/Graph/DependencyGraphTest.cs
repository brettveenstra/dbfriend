using System.Collections.Generic;
using DbFriend.Core.Graph;
using DbFriend.Core.Provider;
using DbFriend.Testing.Unit.Utility;
using MbUnit.Framework;
using Rhino.Mocks;
using DbFriend.Testing.Extensions;

namespace DbFriend.Testing.Unit.Graph
{
    public class DependencyGraphTest : Specification<DependencyGraph>
    {
        [Test]
        public void CanWireUpDependencies()
        {
            IDbObject stubStoredProc = MockRepository.GenerateStub<IDbObject>();
            IDbObject stubTable = MockRepository.GenerateStub<IDbObject>();
            IDbObject stubView = MockRepository.GenerateStub<IDbObject>();

            Sut.AddDependency(stubView, stubTable);
            Sut.AddDependency(stubStoredProc,stubTable);
            Sut.AddDependency(stubStoredProc,stubView);

            List<IDbObject> orderedList = new List<IDbObject>(Sut.Dependencies);

            orderedList.Count.ShouldBe(3);
            Assert.AreEqual(stubTable, orderedList[0]);
            Assert.AreEqual(stubView, orderedList[1]);
            Assert.AreEqual(stubStoredProc, orderedList[2]);
        }

        protected override void Before_Each_Spec()
        {
        }
    }
}