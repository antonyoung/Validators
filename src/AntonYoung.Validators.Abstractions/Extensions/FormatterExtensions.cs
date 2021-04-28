using AntonYoung.Validators.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AntonYoung.Validators.Abstractions.Extensions
{
    /// <summary>
    ///     use provided formatters, in case you don't need hyphen(s) and or whitespace(s). 
    ///     or you want to replace hyphen(s) and or whitespace(s), to your liking?
    /// </summary>
    public static class FormatterExtensions
    {
        /// <summary>
        ///     used as internal logic of available formatters.
        /// </summary>
        private static readonly Dictionary<Formatters, string> _formatters = new Dictionary<Formatters, string>()
        {
            { Formatters.None, string.Empty },
            { Formatters.WhiteSpaces, @"\s+" },
            { Formatters.Hyphens,  @"\-+" },
            { Formatters.HyphensAndWhiteSpaces,  @"[\s+\-+]"}
        };

        /// <summary>
        ///     formats with default formatter <seealso cref="Formatters.None"/>.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with default formatter.
        /// </param>
        /// <returns>
        ///     the formatted value with the default formatter.
        /// </returns>
        public static string Format(this string value)
            => Format(value, Formatters.None, string.Empty);

        /// <summary>
        ///     formats with default formatter <seealso cref="Formatters.None"/>
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with default formatter.
        /// </param>
        /// <returns>
        ///     the formatted value with the default formatter.
        /// </returns>
        public static string Format(this string value, string replace)
            => Format(value, Formatters.None, replace);

        /// <summary>
        ///     formats with default formatter.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with provided formatter <seealso cref="Formatters"/>
        /// </param>
        /// <param name="formatter">
        ///     used as the formatter to be used.
        /// </param>
        /// <exception cref="NotSupportedException(nameof(formatter))"/>
        /// <returns>
        ///     the formatted value.
        /// </returns>
        public static string Format(this string value, Formatters formatter) 
            => Format(value, formatter, string.Empty);

        /// <summary>
        ///     formats with provided formatter with replace string.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with provided formatter.
        /// </param>
        /// <param name="formatter">
        ///     used as the formatter to be used.
        /// </param>
        /// <param name="replace">
        ///     used as string value to be replaced by, selected fomatter.
        /// </param>
        /// <exception cref="NotSupportedException(nameof(formatter))">
        ///     in case provided enum is none existing in <seealso cref="_formatters"/>
        /// </exception>
        /// <returns>
        ///     the formatted value.
        /// </returns>
        public static string Format(this string value, Formatters formatter, string replace)
        {
            if (!_formatters.TryGetValue(formatter, out string regexPattern))
                throw new NotSupportedException(nameof(formatter));

            return Regex.Replace(value, regexPattern, replace);
        }
    }
}