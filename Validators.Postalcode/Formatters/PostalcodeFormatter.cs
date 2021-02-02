using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Validators.Postalcode.Enums;

namespace Validators.Postalcode.Formatters
{
    /// <summary>
    ///     formats postalcodes with a postal code formatter.
    ///     use provided formatters, in case you don't need hyphen(s) and or whitespace(s). 
    ///     or you want to replace hyphen(s) and or whitespace(s), to your linking?
    ///     often an api(s) expect as example The Netherlands without a space "1234AB".
    /// </summary>
    public static class PostalCodeFormatter
    {
        /// <summary>
        ///     used as internal logic of available formatters.
        /// </summary>
        private static readonly Dictionary<PostalcodeFormatters, string> _formatters = new Dictionary<PostalcodeFormatters, string>()
        {
            { PostalcodeFormatters.None, string.Empty },
            { PostalcodeFormatters.WhiteSpaces, @"\s+" },
            { PostalcodeFormatters.Hyphens,  @"\-+" },
            { PostalcodeFormatters.HyphensAndWhiteSpaces,  @"[\s+\-+]"}
        };

        /// <summary>
        ///     formats with default formatter.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with default formatter.
        /// </param>
        /// <returns>
        ///     the formatted value with the default formatter.
        /// </returns>
        public static string Format(this string value) 
            => Format(value, PostalcodeFormatters.None, string.Empty);

        /// <summary>
        ///     formats with default formatter.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with default formatter.
        /// </param>
        /// <returns>
        ///     the formatted value with the default formatter.
        /// </returns>
        public static string Format(this string value, string replace)
            => Format(value, PostalcodeFormatters.None, replace);

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
        public static string Format(this string value, PostalcodeFormatters formatter, string replace)
        {
            if (!_formatters.TryGetValue(formatter, out string matchExpression))
                throw new ArgumentException(nameof(formatter));

            return Regex.Replace(value, matchExpression, replace);
        }
    }
}