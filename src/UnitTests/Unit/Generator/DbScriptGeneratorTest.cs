// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptGeneratorTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptGeneratorTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using DbFriend.Core.Generator;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;
using DbFriend.Core.Provider;
using DbFriend.Testing.Utility;
using MbUnit.Framework;
using Rhino.Mocks;

namespace DbFriend.Testing.Unit.Generator
{
    /// <summary>
    /// </summary>
    public class DbScriptGeneratorTest : Specification<DbScriptGenerator>
    {
        /// <summary>
        /// </summary>
        [Test]
        public void ScriptDb_Top_Down()
        {
            IDatabase connection = MockingContext.Get<IDatabase>();
            IDbScriptOutputPipeline pipeline = MockingContext.Get<IDbScriptOutputPipeline>();
            IDbScriptProvider provider = MockingContext.Get<IDbScriptProvider>();
            IDbScriptFolderConfigurationSetting setting = MockingContext.Get<IDbScriptFolderConfigurationSetting>();
            
            provider.Expect(x => x.ForTheDatabase(connection))
                .IgnoreArguments();

            provider.Expect(x => x.ScriptUsing(pipeline))
                .Return(provider)
                .IgnoreArguments();

            provider.Expect(x => x.NotifyingActionsWith(null))
                .Return(provider)
                .IgnoreArguments();

            provider.Expect(x => x.WithSetting(setting))
                .Return(provider)
                .IgnoreArguments();
            
            Sut.ScriptDb(null);

            provider.VerifyAllExpectations();
        }

        /// <summary>
        /// </summary>
        protected override void Before_Each_Spec()
        {
        }
    }
}