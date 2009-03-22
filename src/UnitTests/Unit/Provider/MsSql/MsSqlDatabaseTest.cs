// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDatabaseTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDatabaseTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Diagnostics;
using DbFriend.Core.Generator.Targets;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Core.Provider.MsSql.Adapters;
using DbFriend.Core.Provider.MsSql.Mappers;
using DbFriend.Testing.Utility;
using MbUnit.Framework;
using Rhino.Mocks;

namespace DbFriend.Testing.Unit.Provider.MsSql
{
    /// <summary>
    /// </summary>
    [TestFixture]
    public class MsSqlDatabaseTest : Specification<MsSqlDatabase>
    {
        private IEnumerable<IMsSqlObject> StubbedEnumerableMsSqlObjects
        {
            get { yield return StubDbObject; }
        }

        private IMsSqlObject StubDbObject
        {
            get { return MockRepository.GenerateMock<IMsSqlObject>(); }
        }

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
            IMsSqlDatabaseConnectionAdapter mockAdapter = MockingContext.Get<IMsSqlDatabaseConnectionAdapter>();
            mockAdapter.Expect(x => x.Connect());
            mockAdapter.Stub(x => x.StoredProcedures).Return(StubbedEnumerableMsSqlObjects);
            mockAdapter.Stub(x => x.Tables).Return(StubbedEnumerableMsSqlObjects);
            mockAdapter.Stub(x => x.Views).Return(StubbedEnumerableMsSqlObjects);
            mockAdapter.Stub(x => x.Functions).Return(StubbedEnumerableMsSqlObjects);
            
            IMsSqlStoredProcStreamWriterAdapterMapper mockStoredProcMapper = MockingContext.Get<IMsSqlStoredProcStreamWriterAdapterMapper>();
            mockStoredProcMapper.Expect(x => x.MapFrom(StubDbObject))
                    .IgnoreArguments()
                    .Return(MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());

            IMsSqlTableStreamWriterAdapterMapper mockTableMapper = MockingContext.Get<IMsSqlTableStreamWriterAdapterMapper>();
            mockTableMapper.Expect(x => x.MapFrom(StubDbObject))
                    .IgnoreArguments()
                    .Return(MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());

            IMsSqlViewStreamWriterAdapterMapper mockViewMapper = MockingContext.Get<IMsSqlViewStreamWriterAdapterMapper>();
            mockViewMapper.Expect(x => x.MapFrom(StubDbObject))
                    .IgnoreArguments()
                    .Return(MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());

            IMsSqlFunctionStreamWriterAdapterMapper mockFunctionMapper = MockingContext.Get<IMsSqlFunctionStreamWriterAdapterMapper>();
            mockFunctionMapper.Expect(x => x.MapFrom(StubDbObject))
                    .IgnoreArguments()
                    .Return(MockingContext.AddAdditionalMockFor<IDbObjectStreamWriterAdapter>());
            
            IDbScriptOutputPipeline mockPipeline = MockRepository.GenerateStub<IDbScriptOutputPipeline>();
            mockPipeline.Expect(x => x.Flush()).Return(StubMessages);

            Sut.ScriptTo(mockPipeline, a => Debug.WriteLine(string.Format("Should_Use_Adapter_To_Script: {0}", a.UpdateMessage)));

            mockAdapter.VerifyAllExpectations();
            mockPipeline.VerifyAllExpectations();

            mockFunctionMapper.VerifyAllExpectations();
            mockViewMapper.VerifyAllExpectations();
            mockTableMapper.VerifyAllExpectations();
            mockStoredProcMapper.VerifyAllExpectations();
        }

        protected override void Before_Each_Spec()
        {
        }
    }
}