using System;
using System.Globalization;

namespace Dell.Service.API.Client.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Noes the null string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string NoNullString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            return str;
        }

        /// <summary>
        /// Creates the relative URI.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static Uri CreateRelativeUri(this string baseAddress, string url)
        {
            return new Uri(new Uri(baseAddress), url);
        }

        public static Uri CreateRelativeUri(this string baseAddress)
        {
            return new Uri(baseAddress);
        }
        /// <summary>
        /// Convert value type into string with invariant culture
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AsString<TValue>(this TValue value) where TValue : struct, IConvertible
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }
    }
}
