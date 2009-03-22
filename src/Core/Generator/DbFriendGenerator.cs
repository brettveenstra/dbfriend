// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DbFriendGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DbFriendGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Diagnostics;
using DbFriend.Core.Provider;
using DbFriend.Core.Provider.MsSql;
using StructureMap;

namespace DbFriend.Core.Generator
{
    /// <summary>
    /// </summary>
    public class DbFriendGenerator : IDbFriendGenerator
    {
        /// <summary>
        /// </summary>
        private readonly DbFriendGeneratorRegistry generatorRegistry;

        /// <summary>
        /// </summary>
        private readonly MsSqlServerRegistry serverRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbFriendGenerator"/> class.
        /// </summary>
        /// <param name="generatorRegistry">
        /// The generatorRegistry.
        /// </param>
        /// <param name="serverRegistry">
        /// The server Registry.
        /// </param>
        public DbFriendGenerator(MsSqlServerRegistry serverRegistry, DbFriendGeneratorRegistry generatorRegistry)
        {
            this.generatorRegistry = generatorRegistry;
            this.serverRegistry = serverRegistry;
        }

        #region IDbFriendGenerator Members

        /// <summary>
        /// </summary>
        /// <param name="notifyAction">
        /// The notify action.
        /// </param>
        public void Generate(Action<IDbScriptObjectUpdate> notifyAction)
        {
            ObjectFactory.Configure(x =>
                                    {
                                        x.AddRegistry(serverRegistry);
                                        x.AddRegistry(generatorRegistry);
                                    });

            IDbScriptGenerator scriptGenerator = ObjectFactory.GetInstance<IDbScriptGenerator>();
            scriptGenerator.ScriptDb(notifyAction);

            IDbSolutionGenerator solutionGenerator = ObjectFactory.GetInstance<IDbSolutionGenerator>();
            solutionGenerator.Generate(x => Debug.WriteLine(x));
        }

        #endregion
    }
}