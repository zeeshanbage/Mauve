using System;
using System.Collections.Generic;

namespace Mauve.Extensibility
{
    /// <summary>
    /// Represents a collection of extension methods for <see cref="IEnumerable{T}"/> instances.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Iterates over the objects within an <see cref="IEnumerable{T}"/> instance and performs the specified action.
        /// </summary>
        /// <typeparam name="T">The type of object contained in the <see cref="IEnumerable{T}"/> instance.</typeparam>
        /// <param name="collection">The collection of objects to work with.</param>
        /// <param name="action">The action to be invoked for each object in the <see cref="IEnumerable{T}"/> instance.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
                action(item);
        }
    }
}
