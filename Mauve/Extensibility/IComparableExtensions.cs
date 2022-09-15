using System;

namespace Mauve.Core.Extensibility
{
    /// <summary>
    /// Represents a collection of extension methods for <see cref="IComparable"/> instances.
    /// </summary>
    public static class IComparableExtensions
    {
        /// <summary>
        /// Determines if the calling object's value is equal to or greater than the specified minimum value, while remaining less than or equal to the specified maximum value.
        /// </summary>
        /// <param name="value">The calling object's value.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <returns>Returns true if the calling object's value is equal to or greater than the specified minimum value, while remaining less than or equal to the specified maximum value, otherwise false.</returns>
        public static bool WithinRange(this IComparable value, IComparable min, IComparable max) => value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }
}
