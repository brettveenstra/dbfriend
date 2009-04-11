// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbScriptGeneratorTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbScriptGeneratorTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Unit.Generator
{
    using DbFriend.Core.Generator;
    using DbFriend.Core.Generator.Settings;
    using DbFriend.Core.Generator.Targets;
    using DbFriend.Core.Provider;

    using MbUnit.Framework;

    using Rhino.Mocks;

    using Utility;

    /// <summary>
    /// </summary>
    public class DbScriptGeneratorTest : Specification<DbScriptGenerator>
    {
        /// <summary>
        /// </summary>
        [Test]
        public void ScriptDb_Top_Down()
        {
            IDatabase connection = this.MockingContext.Get<IDatabase>();
            IDbScriptOutputPipeline pipeline = this.MockingContext.Get<IDbScriptOutputPipeline>();
            IDbScriptProvider provider = this.MockingContext.Get<IDbScriptProvider>();
            IDbScriptFolderConfigurationSetting setting = this.MockingContext.Get<IDbScriptFolderConfigurationSetting>();

            provider.Expect(x => x.ForTheDatabase(connection)).IgnoreArguments();

            provider.Expect(x => x.ScriptUsing(pipeline)).Return(provider).IgnoreArguments();

            provider.Expect(x => x.NotifyingActionsWith(null)).Return(provider).IgnoreArguments();

            provider.Expect(x => x.WithSetting(setting)).Return(provider).IgnoreArguments();

            this.Sut.ScriptDb(null);

            provider.VerifyAllExpectations();
        }

        /// <summary>
        /// </summary>
        protected override void Before_Each_Spec()
        {
        }
    }
}