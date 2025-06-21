using System.ComponentModel.DataAnnotations;

namespace Icomatic.Commons
{
    internal static class EnumUtilities
    {
        /// <summary>
        /// Converts an enum value to a string using the Display attribute name.
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The enum value</param>
        /// <returns>The display name or empty string if not found</returns>
        public static string ToDisplayName<T>(T value) where T : struct
        {
            return GetDisplayAttribute(value).Name ?? string.Empty;
        }

        /// <summary>
        /// Gets the description from the Display attribute of an enum value.
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The enum value</param>
        /// <returns>The description or empty string if not found</returns>
        public static string ToDisplayDescription<T>(T value) where T : struct
        {
            return GetDisplayAttribute(value).Description ?? string.Empty;
        }

        /// <summary>
        /// Gets the Display attribute from an enum value.
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The enum value</param>
        /// <returns>The DisplayAttribute or an empty one if not found</returns>
        public static DisplayAttribute GetDisplayAttribute<T>(T value) where T : struct
        {
            var empty = new DisplayAttribute() { Name = string.Empty, Description = string.Empty };
            var fieldInfo = value.GetType().GetField(value.ToString()!);
            if (fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) is DisplayAttribute[] attributes && attributes.Length > 0)
            {
                return attributes[0] ?? empty;
            }
            return empty;
        }

        /// <summary>
        /// Tries to parse a string to an enum value using the Display attribute name.
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="displayName">The display name to parse</param>
        /// <param name="result">The parsed enum value</param>
        /// <returns>True if parsing succeeded, false otherwise</returns>
        public static bool TryParseFromDisplayName<T>(string displayName, out T result) where T : struct, Enum
        {
            result = default;
            if (string.IsNullOrWhiteSpace(displayName))
                return false;

            foreach (T enumValue in Enum.GetValues<T>())
            {
                if (string.Equals(ToDisplayName(enumValue), displayName, StringComparison.OrdinalIgnoreCase))
                {
                    result = enumValue;
                    return true;
                }
            }

            return false;
        }
    }
}