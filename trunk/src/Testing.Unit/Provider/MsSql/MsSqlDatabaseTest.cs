// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDatabaseTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDatabaseTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Unit.Provider.MsSql
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using DbFriend.Core.Generator.Targets;
    using DbFriend.Core.Provider.MsSql;
    using DbFriend.Core.Provider.MsSql.Adapters;
    using DbFriend.Core.Provider.MsSql.Mappers;

    using MbUnit.Framework;

    using Rhino.Mocks;

    using Utility;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class MsSqlDatabaseTest : Specification<MsSqlDatabaseScripter>
    {
        /// <summary>
        /// Gets StubbedEnumerableMsSqlObjects.
        /// </summary>
        /// <value>
        /// The stubbed enumerable ms sql objects.
        /// </value>
        private IEnumerable<IMsSqlObject> StubbedEnumerableMsSqlObjects
        {
            get
            {
                yield return this.StubDbObject;
            }
        }

        /// <summary>
        /// Gets StubDbObject.
        /// </summary>
        /// <value>
        /// The stub db object.
        /// </value>
        private IMsSqlObject StubDbObject
        {
            get
            {
                return MockRepository.GenerateMock<IMsSqlObject>();
            }
        }

        /// <summary>
        /// Gets StubMessages.
        /// </summary>
        /// <value>
        /// The stub messages.
        /// </value>
        private IEnumerable<string> StubMessages
        {
            get
            {
                yield return "foo";
                yield return "bar";
                yield return "baz";
            }
        }

        /// <summary>
        /// </summary>
        [Test]
        public void Should_Use_Adapter_To_Script()
        {
            IMsSqlDatabaseConnectionAdapter mockAdapter = this.MockingContext.Get<IMsSqlDatabaseConnectionAdapter>();
            mockAdapter.Expect(x => x.Connect());
            mockAdapter.Stub(x => x.StoredProcedures).Return(this.StubbedEnumerableMsSqlObjects);
            mockAdapter.Stub(x => x.Tables).Return(this.StubbedEnumerableMsSqlObjects);
            mockAdapter.Stub(x => x.Views).Return(this.StubbedEnumerableMsSqlObjects);
            mockAdapter.Stub(x => x.Functions).Return(this.StubbedEnumerableMsSqlObjects);

            IMsSqlStoredProcStreamWriterAdapterMapper mockStoredProcMapper = this.MockingContext.Get<IMsSqlStoredProcStreamWriterAdapterMapper>();
            mockStoredProcMapper.Expect(x => x.MapFrom(this.StubDbObject)).IgnoreArguments().Return(
                    this.MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());

            IMsSqlTableStreamWriterAdapterMapper mockTableMapper = this.MockingContext.Get<IMsSqlTableStreamWriterAdapterMapper>();
            mockTableMapper.Expect(x => x.MapFrom(this.StubDbObject)).IgnoreArguments().Return(
                    this.MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());

            IMsSqlViewStreamWriterAdapterMapper mockViewMapper = this.MockingContext.Get<IMsSqlViewStreamWriterAdapterMapper>();
            mockViewMapper.Expect(x => x.MapFrom(this.StubDbObject)).IgnoreArguments().Return(
                    this.MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());

            IMsSqlFunctionStreamWriterAdapterMapper mockFunctionMapper = this.MockingContext.Get<IMsSqlFunctionStreamWriterAdapterMapper>();
            mockFunctionMapper.Expect(x => x.MapFrom(this.StubDbObject)).IgnoreArguments().Return(
                    this.MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());

            IDbScriptOutputPipeline mockPipeline = MockRepository.GenerateStub<IDbScriptOutputPipeline>();
            mockPipeline.Expect(x => x.Flush()).Return(this.StubMessages);

            this.Sut.ScriptTo(mockPipeline, a => Debug.WriteLine(string.Format("Should_Use_Adapter_To_Script: {0}", a.UpdateMessage)));

            mockAdapter.VerifyAllExpectations();
            mockPipeline.VerifyAllExpectations();

            mockFunctionMapper.VerifyAllExpectations();
            mockViewMapper.VerifyAllExpectations();
            mockTableMapper.VerifyAllExpectations();
            mockStoredProcMapper.VerifyAllExpectations();
        }

        /// <summary>
        /// </summary>
        protected override void Before_Each_Spec()
        {
        }
    }
}