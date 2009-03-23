// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptFileTargetTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptFileTargetTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using DbFriend.Core.Generator.Targets;
using MbUnit.Framework;
using Rhino.Mocks;

namespace DbFriend.Testing.Unit.Generator
{
    /// <summary>
    /// </summary>
    [TestFixture]
    public class DbScriptOutputFolderPipelineTest
    {
        /// <summary>
        /// </summary>
        [Test]
        public void Should_Flush_DbScriptObjects_Using_FileStream()
        {
            // arrange
            IDbObjectStreamWriterAdapter adapter = MockRepository.GenerateMock<IDbObjectStreamWriterAdapter>();
            adapter.Expect(x => x.Write()).Return("foo");

            IDbScriptFolderManager manager = MockRepository.GenerateMock<IDbScriptFolderManager>();
            manager.Expect(x => x.Prepare());
            
            // act
            DbScriptOutputFolderPipeline sut = new DbScriptOutputFolderPipeline(manager);
            sut.WireIn(adapter);
            List<string> flush = new List<string>(sut.Flush());

            // assert
            adapter.VerifyAllExpectations();
            manager.VerifyAllExpectations();

            Assert.AreEqual("foo", flush[0]);
        }
    }
}