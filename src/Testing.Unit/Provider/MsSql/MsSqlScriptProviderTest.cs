// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlScriptProviderTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlScriptProvider type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Unit.Provider.MsSql
{
    using System;

    using DbFriend.Core.Generator.Settings;
    using DbFriend.Core.Generator.Targets;
    using DbFriend.Core.Provider.MsSql;

    using MbUnit.Framework;

    using Rhino.Mocks;

    using StructureMap;

    using Utility;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class MsSqlScriptProviderTest : Specification<MsSqlScriptProvider>
    {
        /// <summary>
        /// </summary>
        [Test]
        public void Should_Script_Db_In_EasyWay_UsingFactory()
        {
            IMsSqlDatabase database = this.MockingContext.Get<IMsSqlDatabase>();
            IDbScriptOutputPipeline pipeline = this.MockingContext.AddAdditionalMockFor<IDbScriptOutputPipeline>();
            IDbScriptFolderConfigurationSetting setting = this.MockingContext.Get<IDbScriptFolderConfigurationSetting>();

            database.Expect(x => x.ScriptTo(null, null)).IgnoreArguments();

            // act
            this.Sut.ScriptUsing(pipeline).WithSetting(setting).NotifyingActionsWith(null).ForTheDatabase(database);

            database.VerifyAllExpectations();
        }

        /// <summary>
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException), "Must initialize a IDbScriptOutputPipeline")]
        public void Should_Check_That_Setting_Necessary_Dependencies_Through_FluentInterface()
        {
            // act
            this.Sut.ScriptUsing(null).NotifyingActionsWith(null).ForTheDatabase(this.MockingContext.Get<IMsSqlDatabase>());
        }

        /// <summary>
        /// </summary>
        protected override void Before_Each_Spec()
        {
            ObjectFactory.Initialize(x => x.AddRegistry<UnitTestRegistry>());
        }
    }
}