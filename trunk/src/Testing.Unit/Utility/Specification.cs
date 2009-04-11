// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Specification.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Specification type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Unit.Utility
{
    using MbUnit.Framework;

    using StructureMap.AutoMocking;

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
            get
            {
                return this.mockingContext;
            }
        }

        /// <summary>
        /// Gets Sut.
        /// </summary>
        /// <value>
        /// The sut.
        /// </value>
        protected T Sut
        {
            get
            {
                return this.sut;
            }
        }

        /// <summary>
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.mockingContext = new RhinoAutoMocker<T>(MockMode.AAA);
            this.sut = this.mockingContext.ClassUnderTest;

            this.Before_Each_Spec();
        }

        /// <summary>
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.After_Each_Spec();
        }

        /// <summary>
        /// Add an additional Mock Object for the Type requested.
        /// </summary>
        /// <typeparam name="U">Type to mock</typeparam>
        /// <returns>Mocked Type</returns>
        protected U MockA<U>() where U : class
        {
            return this.mockingContext.AddAdditionalMockFor<U>();
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