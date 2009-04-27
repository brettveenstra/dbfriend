// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlTableTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlTableTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Testing.Unit.Provider.MsSql
{
    using System.Collections.Generic;

    using DbFriend.Core.Provider.MsSql;
    using DbFriend.Testing.Extensions;

    using MbUnit.Framework;

    using Rhino.Mocks;

    using Utility;

    /// <summary>
    /// </summary>
    public class MsSqlTableTest : Specification<MsSqlTable>
    {
        /// <summary>
        /// Gets StubEnumerableMsSqlObjects.
        /// </summary>
        /// <value>
        /// The stub enumerable ms sql objects.
        /// </value>
        protected IEnumerable<IMsSqlObject> StubEnumerableMsSqlObjects
        {
            get
            {
                yield return MockRepository.GenerateStub<IMsSqlObject>();
                yield return MockRepository.GenerateStub<IMsSqlObject>();
            }
        }

        /// <summary>
        /// </summary>
        protected override void Before_Each_Spec()
        {
        }
    }
}