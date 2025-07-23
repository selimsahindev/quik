using System;
using System.Globalization;
using System.Linq;

namespace quik.Runtime.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the string is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// Checks if the string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// Converts the string to title case (first letter of each word capitalized).
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Removes all white-space characters from the string.
        /// </summary>
        public static string RemoveWhitespace(this string str)
        {
            return string.IsNullOrEmpty(str) ? str : string.Concat(str.Where(c => !char.IsWhiteSpace(c)));
        }

        /// <summary>
        /// Converts the string to lowercase and then checks if it contains the given substring (case-insensitive).
        /// </summary>
        public static bool ContainsIgnoreCase(this string str, string value)
        {
            if (str == null || value == null)
            {
                return false;
            }
            return str.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Truncates the string to the specified length, adding "..." at the end if truncated.
        /// </summary>
        public static string Truncate(this string str, int length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return str.Length <= length ? str : str.Substring(0, length) + "...";
        }

        /// <summary>
        /// Converts a hex color string (e.g., "#FFFFFF") to a Unity Color.
        /// </summary>
        public static UnityEngine.Color ToColor(this string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return UnityEngine.Color.black;
            }

            hex = hex.Replace("#", string.Empty);
            if (hex.Length == 6)
            {
                hex = "FF" + hex; // Add alpha if it's missing
            }
            
            byte a = Convert.ToByte(hex.Substring(0, 2), 16);
            byte r = Convert.ToByte(hex.Substring(2, 2), 16);
            byte g = Convert.ToByte(hex.Substring(4, 2), 16);
            byte b = Convert.ToByte(hex.Substring(6, 2), 16);

            return new UnityEngine.Color32(r, g, b, a);
        }

        /// <summary>
        /// Converts a string to a boolean value. Returns false if the conversion fails.
        /// </summary>
        public static bool ToBool(this string str)
        {
            return bool.TryParse(str, out bool result) && result;
        }

        /// <summary>
        /// Safely converts a string to an integer, returning 0 if the conversion fails.
        /// </summary>
        public static int ToIntSafe(this string str)
        {
            return int.TryParse(str, out int result) ? result : 0;
        }

        /// <summary>
        /// Checks if the string starts with the specified prefix, case-insensitive.
        /// </summary>
        public static bool StartsWithIgnoreCase(this string str, string value)
        {
            if (str == null || value == null)
            {
                return false;
            }
            return str.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Replaces the first occurrence of a specified substring with another substring.
        /// </summary>
        public static string ReplaceFirst(this string str, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(oldValue))
            {
                return str;
            }
            
            int index = str.IndexOf(oldValue, StringComparison.Ordinal);
            if (index < 0)
            {
                return str;
            }
            
            return str.Substring(0, index) + newValue + str.Substring(index + oldValue.Length);
        }
    }
}
