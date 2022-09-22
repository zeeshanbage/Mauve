using System;

using Mauve.Extensibility;

namespace Mauve
{
    /// <summary>
    /// Represents a <see cref="DateFormat"/> for creating <see cref="string"/> representations of <see cref="DateTime"/> instances.
    /// </summary>
    public enum DateFormat
    {
        /// <summary>
        /// No format specified.
        /// </summary>
        /// <remarks>Calls to <see cref="DateTimeExtensions.Format(DateTime, DateFormat)"/> will simply invoke <see cref="DateTime.ToString()"/>.</remarks>
        None = 0
    }
}
