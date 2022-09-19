using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mauve.Extensibility
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
                action(item);
        }
    }
}
