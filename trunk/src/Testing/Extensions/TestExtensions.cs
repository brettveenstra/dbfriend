using System;
using MbUnit.Framework;

namespace DbFriend.Testing.Extensions
{
    public static class TestExtensions
    {
        public static void ShouldBe(this int value, int expected)
        {
            Assert.AreEqual(expected, value);
        }

        public static void ShouldBe(this string value, string expected)
        {
            Assert.AreEqual(expected, value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static void ShouldBeExactly(this string value, string expected)
        {
            Assert.AreEqual(expected, value, StringComparison.CurrentCulture);
        }

        public static void ShouldBeTrue(this bool value)
        {
            Assert.IsTrue(value);
        }

        public static void ShouldBeFalse(this bool value)
        {
            Assert.IsFalse(value);
        }
    }
}