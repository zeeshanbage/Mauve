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
        None = 0,
        /// <summary>
        /// ISO 8601 is an international standard covering the worldwide exchange and communication of date and time-related data.
        /// </summary>
        /// <remarks>Calls to <see cref="DateTimeExtensions.Format(DateTime, DateFormat)"/> will use the format: <c>yyyy-MM-ddTHH:mm:ss.ffK</c>.</remarks>
        /// <see href="https://en.wikipedia.org/wiki/ISO_8601"/>
        Iso8601 = 1
    }
}
