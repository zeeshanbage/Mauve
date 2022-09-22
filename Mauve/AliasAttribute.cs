using System;

namespace Mauve
{
    /// <summary>
    /// An <see cref="Attribute"/> designed for aliasing objects and their members.
    /// </summary>
    public class AliasAttribute : Attribute
    {
        /// <summary>
        /// The alias assigned to the object or member this attribute is applied to.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Creates a new <see cref="AliasAttribute"/> instance with the specified alias.
        /// </summary>
        /// <param name="alias">The alias assigned to the object or member this attribute is applied to.</param>
        public AliasAttribute(string value) => Value = value;
    }
}
