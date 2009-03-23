// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Specification.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Specification type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using MbUnit.Framework;
using StructureMap.AutoMocking;

namespace DbFriend.Testing.Utility
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class Specification<T> where T : class
    {
        /// <summary>
        /// </summary>
        private RhinoAutoMocker<T> mockingContext;

        /// <summary>
        /// </summary>
        private T sut;

        /// <summary>
        /// Gets MockingContext.
        /// </summary>
        /// <value>
        /// The mockingContext.
        /// </value>
        protected RhinoAutoMocker<T> MockingContext
        {
            get { return mockingContext; }
        }

        /// <summary>
        /// Gets Sut.
        /// </summary>
        /// <value>
        /// The sut.
        /// </value>
        protected T Sut
        {
            get { return sut; }
        }

        /// <summary>
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockingContext = new RhinoAutoMocker<T>(MockMode.AAA);
            sut = mockingContext.ClassUnderTest;

            Before_Each_Spec();
        }

        /// <summary>
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            After_Each_Spec();
        }

        /// <summary>
        /// Add an additional Mock Object for the Type requested.
        /// </summary>
        /// <typeparam name="U">Type to mock</typeparam>
        /// <returns>Mocked Type</returns>
        protected U MockA<U>() where U : class
        {
            return mockingContext.AddAdditionalMockFor<U>();
        }

        /// <summary>
        /// </summary>
        protected virtual void After_Each_Spec()
        {
        }

        /// <summary>
        /// </summary>
        protected abstract void Before_Each_Spec();
    }
}