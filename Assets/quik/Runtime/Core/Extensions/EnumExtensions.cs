using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace quik.Runtime.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description attribute of an enum value.
        /// Returns the enum name if no description is found.
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null) return value.ToString();

            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attr?.Description ?? value.ToString();
        }

        /// <summary>
        /// Returns all values of an enum type as an array.
        /// </summary>
        public static T[] GetValues<T>() where T : Enum => (T[])Enum.GetValues(typeof(T));

        /// <summary>
        /// Returns all names of an enum type as a string array.
        /// </summary>
        public static string[] GetNames<T>() where T : Enum => Enum.GetNames(typeof(T));

        /// <summary>
        /// Checks whether a flag is set in the enum.
        /// Safer and faster than using Enum.HasFlag.
        /// </summary>
        public static bool HasFlagFast<T>(this T value, T flag) where T : Enum
        {
            var valueInt = Convert.ToUInt64(value);
            var flagInt = Convert.ToUInt64(flag);
            return (valueInt & flagInt) == flagInt;
        }

        /// <summary>
        /// Parses a string to an enum value. Returns default(T) if parsing fails.
        /// </summary>
        public static T ParseOrDefault<T>(this string input, T defaultValue = default) where T : struct, Enum
        {
            return Enum.TryParse<T>(input, true, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Converts an enum to a dictionary of its names and values.
        /// </summary>
        public static Dictionary<string, T> ToDictionary<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .ToDictionary(e => e.ToString(), e => e);
        }
    }
}
