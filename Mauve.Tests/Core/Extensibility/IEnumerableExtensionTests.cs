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
                guids.ForEach(guid => Console.WriteLine(guid));
            } catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
