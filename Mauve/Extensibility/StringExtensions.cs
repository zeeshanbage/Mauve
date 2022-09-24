using System;

using Mauve.Serialization;

namespace Mauve.Extensibility
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether this string and an input string contain the same value. A parameter specifies if case should be ignored.
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Contains(this string firstString, string secondString, bool ignoreCase)
        {
            if (ignoreCase)
            {
                // If ignoreCase is set to true, we'll ignore the case
                return firstString.IndexOf(secondString, StringComparison.InvariantCultureIgnoreCase) >= 0;
            } else
            {
                // If ignoreCase is false/not set, we won't ignore the case
                return firstString.IndexOf(secondString, StringComparison.InvariantCulture) >= 0;
            }
        }
        /// <summary>
        /// Deserializes the specified input utilizing the specified <see cref="SerializationMethod"/>.
        /// </summary>
        /// <typeparam name="T">The type of the data to be deserialized.</typeparam>
        /// <param name="input">The serialized data to be deserialized.</param>
        /// <param name="serializationMethod">The <see cref="SerializationMethod"/> that should be utilized for deserialization.</param>
        /// <returns>Returns the input deserialized using the specified <see cref="SerializationMethod"/>.</returns>
        public static T Deserialize<T>(this string input, SerializationMethod serializationMethod)
        {
            SerializationProvider serializationProvider;
            switch (serializationMethod)
            {
                case SerializationMethod.Binary: serializationProvider = new BinarySerializationProvider(); break;
                case SerializationMethod.Xml: serializationProvider = new XmlSerializationProvider(); break;
                case SerializationMethod.Json: serializationProvider = new JsonSerializationProvider(); break;
                case SerializationMethod.Yaml: serializationProvider = null; break;
                default: serializationProvider = new RawSerializationProvider(); break;
            }

            return serializationProvider.Deserialize<T>(input);
        }
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
