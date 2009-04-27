// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="DependencyGeneratorTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DependencyGeneratorTest type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Integration.Provider.MsSql
{
    using System.Collections.Generic;
    using Core.Graph;
    using Core.Provider;
    using Core.Provider.MsSql;
    using Extensions;
    using MbUnit.Framework;
    using StructureMap;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class DependencyGeneratorTest
    {
        /// <summary>
        /// </summary>
        [Test]
        public void Exercise()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new IntegrationTestRegistry(MsSqlCredentialMethod.SqlUser)));

            var dependencyGenerator = ObjectFactory.GetInstance<IDependencyGenerator>();
            IDependencyGraph graph = dependencyGenerator.GenerateGraph();

            var orderedDependencies = new List<IDbObject>(graph.Dependencies);

            orderedDependencies[0].Name.ShouldBe("t_Blog");
            orderedDependencies[1].Name.ShouldBe("log_Activity");
            orderedDependencies[2].Name.ShouldBe("p_Blog_GetAll");
            orderedDependencies[3].Name.ShouldBe("t_Post");
            orderedDependencies[4].Name.ShouldBe("fnt_GetPosts");
            orderedDependencies[5].Name.ShouldBe("v_BlogPost");

            orderedDependencies.Count.ShouldBe(6);
        }
    }
}