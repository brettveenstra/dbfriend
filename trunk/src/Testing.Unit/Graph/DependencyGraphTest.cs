// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DependencyGraphTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DependencyGraphTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Testing.Unit.Graph
{
    using System.Collections.Generic;
    using Core.Graph;
    using Core.Provider;
    using Extensions;
    using MbUnit.Framework;
    using Utility;

    /// <summary>
    /// </summary>
    public class DependencyGraphTest : Specification<DependencyGraph>
    {
        /// <summary>
        /// </summary>
        [Test]
        public void CanWireUpDependencies()
        {
            IDbObject stubStoredProc = new DbObject(123, "foo", "sp", "dbo");
            IDbObject stubTable = new DbObject(456, "bar", "u", "dbo");
            IDbObject stubView = new DbObject(789, "baz", "v", "bat");

            Sut.AddDependency(stubView, stubTable);
            Sut.AddDependency(stubStoredProc, stubTable);
            Sut.AddDependency(stubStoredProc, stubView);

            var orderedList = new List<IDbObject>(Sut.Dependencies);

            orderedList.Count.ShouldBe(3);
            Assert.AreEqual(stubTable, orderedList[0]);
            Assert.AreEqual(stubView, orderedList[1]);
            Assert.AreEqual(stubStoredProc, orderedList[2]);
        }

        /// <summary>
        /// </summary>
        protected override void Before_Each_Spec()
        {
        }
    }
}