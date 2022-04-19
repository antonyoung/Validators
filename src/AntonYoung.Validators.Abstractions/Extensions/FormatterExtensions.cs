using AntonYoung.Validators.Abstractions.Constants;
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
        private static readonly IDictionary<Formatters, string> _formatters = new Dictionary<Formatters, string>
        {
            { Formatters.None, new Guid().ToString() },
            { Formatters.WhiteSpaces, Replaces.WhiteSpaces },
            { Formatters.Hyphens,  Replaces.Hyphens },
            { Formatters.HyphensAndWhiteSpaces,  Replaces.HyphensAndWhiteSpaces }
        };

        /// <summary>
        ///     formats <paramref name="value"/> with <see cref="Formatters"/> default as <see cref="Formatters.None"/>.
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with default formatter.
        /// </param>
        /// <returns>
        ///     the formatted value with the default formatter.
        /// </returns>
        public static string Format(this string value)
            => Format(value, Formatters.None);

        /// <summary>
        ///     formats <paramref name="value"/> with provided <paramref name="formatter"/> with deafult replace value as <see cref="string.Empty"/>. 
        /// </summary>
        /// <param name="value">
        ///     used as the value that has to be formmatted with provided formatter <seealso cref="Formatters"/>
        /// </param>
        /// <param name="formatter">
        ///     used as the formatter to be used.
        /// </param>
        /// <exception cref="NotSupportedException"/>
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
        /// <exception cref="NotSupportedException"/>
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