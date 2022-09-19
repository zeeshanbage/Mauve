using Mauve.Extensibility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mauve.Tests.Core.Extensibility
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        [DataRow("this-is-a-test", "THIS-IS-A-TEST", false, false)]
        [DataRow("this-is-a-test", "THIS-IS-A-TEST", true, true)]
        public void EqualsTest(string first, string second, bool ignoreCase, bool expected)
        {
            var result = first.Equals(second, ignoreCase);

            Assert.AreEqual(expected, result);
        }
    }
}
