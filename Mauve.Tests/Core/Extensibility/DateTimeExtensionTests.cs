using System;

using Mauve.Extensibility;
using Mauve.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mauve.Tests.Core.Extensibility
{
    [TestClass]
    public class DateTimeExtensionTests
    {
        [TestMethod("ISO8601")]
        public void Iso8601()
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            int day = now.Day;
            int hour = now.Hour;
            int minute = now.Minute;
            int second = now.Second;
            string millisecond = now.Millisecond.ToString().Substring(0, 2);
            string nowIso8601 = now.ToString(DateFormat.Iso8601);

            // Use SubstringQueue to quickly parse the individual chunks.
            // yyyy-MM-ddTHH:mm:ss.ffK
            var queue = new SubstringQueue(nowIso8601);
            queue.Next(4, out int parsedYear)
                 .Skip(1)
                 .Next(2, out int parsedMonth)
                 .Skip(1)
                 .Next(2, out int parsedDay)
                 .Skip(1)
                 .Next(2, out int parsedHour)
                 .Skip(1)
                 .Next(2, out int parsedMinute)
                 .Skip(1)
                 .Next(2, out int parsedSecond)
                 .Skip(1)
                 .Next(2, out string parsedMillisecond)
                 .Remainder(out string _);

            Assert.AreEqual(year, parsedYear);
            Assert.AreEqual(month, parsedMonth);
            Assert.AreEqual(day, parsedDay);
            Assert.AreEqual(hour, parsedHour);
            Assert.AreEqual(minute, parsedMinute);
            Assert.AreEqual(second, parsedSecond);
            Assert.AreEqual(millisecond, parsedMillisecond);
        }
        [TestMethod("RFC3339")]
        public void Rfc3339()
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            int day = now.Day;
            int hour = now.Hour;
            int minute = now.Minute;
            int second = now.Second;
            string millisecond = now.Millisecond.ToString().Substring(0, 3);
            string nowRfc3339 = now.ToString(DateFormat.Rfc3339);

            // Use SubstringQueue to quickly parse the individual chunks.
            // yyyy-MM-dd'T'HH:mm:ss.fffK
            var queue = new SubstringQueue(nowRfc3339);
            queue.Next(4, out int parsedYear)
                 .Skip(1)
                 .Next(2, out int parsedMonth)
                 .Skip(1)
                 .Next(2, out int parsedDay)
                 .Skip(1) // The 'T' is somehow written as just T.
                 .Next(2, out int parsedHour)
                 .Skip(1)
                 .Next(2, out int parsedMinute)
                 .Skip(1)
                 .Next(2, out int parsedSecond)
                 .Skip(1)
                 .Next(3, out string parsedMillisecond)
                 .Remainder(out string _);

            Assert.AreEqual(year, parsedYear);
            Assert.AreEqual(month, parsedMonth);
            Assert.AreEqual(day, parsedDay);
            Assert.AreEqual(hour, parsedHour);
            Assert.AreEqual(minute, parsedMinute);
            Assert.AreEqual(second, parsedSecond);
            Assert.AreEqual(millisecond, parsedMillisecond);
        }
    }
}
