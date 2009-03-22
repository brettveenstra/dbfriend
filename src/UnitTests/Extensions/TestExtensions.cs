using System;
using MbUnit.Framework;

namespace DbFriend.Testing.Extensions
{
    public static class TestExtensions
    {
        public static void ShouldBe(this int value, int compareTo)
        {
            Assert.AreEqual(value, compareTo);
        }

        public static void ShouldBe(this string value, string compareTo)
        {
            Assert.AreEqual(value, compareTo, StringComparison.CurrentCultureIgnoreCase);
        }

        public static void ShouldBeExactly(this string value, string compareTo)
        {
            Assert.AreEqual(value, compareTo, StringComparison.CurrentCulture);
        }
    }
}