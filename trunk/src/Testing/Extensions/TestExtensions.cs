// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="TestExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the TestExtensions type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Testing.Extensions
{
    using System;

    using MbUnit.Framework;

    /// <summary>
    /// TestExtensions definition 
    /// </summary>
    public static class TestExtensions
    {
        /// <summary>
        /// Asserts integer value is expected
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        public static void ShouldBe(this int value, int expected)
        {
            Assert.IsTrue(expected == value);
        }

        /// <summary>
        /// Asserts string value is expected (Ignorning Case)
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        public static void ShouldBe(this string value, string expected)
        {
            Assert.AreEqual(expected, value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Asserts string value is expected (Case sensitive)
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        public static void ShouldBeExactly(this string value, string expected)
        {
            Assert.AreEqual(expected, value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Asserts boolean value is True
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void ShouldBeTrue(this bool value)
        {
            Assert.IsTrue(value);
        }

        /// <summary>
        /// Asserts boolean value is False
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void ShouldBeFalse(this bool value)
        {
            Assert.IsFalse(value);
        }
    }
}