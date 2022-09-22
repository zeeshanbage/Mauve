using System;

namespace Mauve.Extensibility
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether this string and an input string have the same value. A parameter specifies if case should be ignored.
        /// </summary>
        /// <param name="input">The input string used to check equality with.</param>
        /// <param name="target">The target of the equality check.</param>
        /// <param name="ignoreCase">Should character case be ignored?</param>
        /// <returns>Returns <see langword="true"/> if the <paramref name="input"/> and <paramref name="target"/> are equal, with respect to the <paramref name="ignoreCase"/> specification, otherwise <see langword="false"/>.</returns>
        public static bool Equals(this string input, string target, bool ignoreCase)
        {
            // Create a comparison type based on whether or not we should ignore case.
            StringComparison comparisonType = ignoreCase ?
                StringComparison.InvariantCultureIgnoreCase :
                StringComparison.InvariantCulture;

            // Return whether or not the input string is equal to the comparison string, with respect to the comparison type.
            return input.Equals(target, comparisonType);
        }
    }
}
