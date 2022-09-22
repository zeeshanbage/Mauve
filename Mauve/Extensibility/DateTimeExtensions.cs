using System;

namespace Mauve.Extensibility
{
    /// <summary>
    /// Represents a collection of extension methods for <see cref="DateTime"/> instances.
    /// </summary>
    public static class DateTimeExtensions
    {

        #region Public Methods

        /// <summary>
        /// Translates the specified <see cref="DateTime"/> instance to a string using a specified <see cref="DateFormat"/>.
        /// </summary>
        /// <param name="input">The <see cref="DateTime"/> instance to translate.</param>
        /// <param name="format">The <see cref="DateFormat"/> to translate to.</param>
        /// <returns>Returns the specified <see cref="DateTime"/> instance translated to a <see cref="string"/> using the specified <see cref="DateFormat"/>.</returns>
        public static string Format(this DateTime input, DateFormat format) => input.ToString(GetFormatSpecifier(format));

        #endregion

        #region Private Methods

        private static string GetFormatSpecifier(DateFormat format)
        {
            switch (format)
            {
                case DateFormat.Iso8601: return "yyyy-MM-ddTHH:mm:ss.ffK";
                case DateFormat.Rfc3339: return "yyyy-MM-dd'T'HH:mm:ss.fffK";
                case DateFormat.UnixMilliseconds: return "";
                case DateFormat.MsSql: return "yyyy-MM-dd HH:mm:ss.fff";
                default: return string.Empty;
            }
        }

        #endregion

    }
}
