using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AntonYoung.Validators.Iban.Formatters
{
    /// <summary>
    ///     available formatters for postalcode.
    /// </summary>
    public enum PostalcodeFormatters
    {
        None,                       // none: keep as original with hyphens and or whitespaces. 
        Hyphens,                    // removes: hyphens
        HyphensAndWhiteSpaces,      // removes: hyphens and whitespaces
        WhiteSpaces,                // removes: whitespaces.
    }

    /// <summary>
    ///     formats postalcodes with a postalcode formatter.
    ///     use provided formatters, in case you don't need hyphens and or whitespaces. 
    ///     often an api expects for example The Netherlands without a space "1234AB".
    /// </summary>
    public static class RemoveFormatter
    {
        /// <summary>
        ///     used as internal logic of available formatters.
        /// </summary>
        private static readonly Dictionary<PostalcodeFormatters, string> _formatters = new Dictionary<PostalcodeFormatters, string>()
        {
            { PostalcodeFormatters.None, string.Empty },
            { PostalcodeFormatters.WhiteSpaces, @"\s+" },
            { PostalcodeFormatters.Hyphens,  @"\-+" },
            { PostalcodeFormatters.HyphensAndWhiteSpaces,  @"[\s+-+]"}
        };

        /// <summary>
        ///     formats with default formatter.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with default formatter.
        /// </param>
        /// <returns>
        ///     the formatted value with the default formatter.,
        /// </returns>
        public static string Format(this string value)
            => Format(value, PostalcodeFormatters.None);

        /// <summary>
        ///     formats with default formatter.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with provided formatter.
        /// </param>
        /// <param name="formatter">
        ///     used as the formatter to be used.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <returns>
        ///     the formatted value.
        /// </returns>
        public static string Format(this string value, PostalcodeFormatters formatter)
        {
            if (!_formatters.TryGetValue(formatter, out string replaceExpression))
                throw new ArgumentException(nameof(formatter));

            return Regex.Replace(value, replaceExpression, string.Empty);
        }
    }
}