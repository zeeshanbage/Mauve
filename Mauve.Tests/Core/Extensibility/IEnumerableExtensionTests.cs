using System;

using Mauve.Extensibility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mauve.Tests.Core.Extensibility
{
    [TestClass]
    public class IEnumerableExtensionTests
    {
        [TestMethod("Write Each")]
        public void WriteEach()
        {
            var guids = new Guid[100];
            for (int i = 0; i < 100; i++)
                guids[i] = Guid.NewGuid();

            try
            {
                int iteratedGuids = 0;
                guids.ForEach(guid => Console.WriteLine($"{++iteratedGuids}: {guid}"));
                Assert.AreEqual(guids.Length, iteratedGuids);
            } catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
