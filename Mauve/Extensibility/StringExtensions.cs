using System;

namespace Mauve.Extensibility
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether this string and an input string have the same value. A parameter specifies if case should be ignored.
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Equals(this string firstString, string secondString, bool ignoreCase)
        {
            if(ignoreCase)
            {
                return firstString.Equals(secondString, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                return firstString.Equals(secondString, StringComparison.InvariantCulture);
            }
        }
    }
}
