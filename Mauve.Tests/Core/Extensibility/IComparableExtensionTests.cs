using System;

using Mauve.Extensibility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mauve.Tests.Core.Extensibility
{
    [TestClass]
    public class IComparableExtensionTests
    {

        #region Constants

        private const int LowerBound = 1;
        private const int UpperBound = 10;

        #endregion

        #region Tests

        [TestMethod("Range Exceeded")] public void RangeExceeded() => Assert.IsFalse(15.WithinRange(LowerBound, UpperBound));
        [TestMethod("Within Range")] public void WithinRange() => Assert.IsTrue(3.WithinRange(LowerBound, UpperBound));

        #endregion

    }
}
