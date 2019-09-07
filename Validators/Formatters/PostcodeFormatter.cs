using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Validators.Formatters
{
    /// <summary>
    ///     available formatters for postcode.
    /// </summary>
    public enum PostcodeFormatters
    {
        None,
        Hyphens,
        HyphensAndWhiteSpaces,
        WitheSpaces,
    }


    /// <summary>
    ///     formats postalcodes with a postalcode formatter.
    /// </summary>
    public static class PostcodeFormatter
    {

        /// <summary>
        ///     used as internal logic of available formatters.
        /// </summary>
        private static readonly Dictionary<PostcodeFormatters, string> _formatters = new Dictionary<PostcodeFormatters, string>()
        {
            { PostcodeFormatters.None, string.Empty },
            { PostcodeFormatters.WitheSpaces, @"\s+" },
            { PostcodeFormatters.Hyphens,  @"\-+" },
            { PostcodeFormatters.HyphensAndWhiteSpaces,  @"(\s+|\-+)"}
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
            => Format(value, PostcodeFormatters.None);


        /// <summary>
        ///     formats with default formatter.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with provided formatter.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <returns>
        ///     the formatted valu.
        /// </returns>
        public static string Format(this string value, PostcodeFormatters formatter)
        {
            if (!_formatters.TryGetValue(formatter, out string replaceExpression))
                throw new ArgumentException(nameof(formatter));

            return Regex.Replace(value, replaceExpression, string.Empty);
        }
    }
}
