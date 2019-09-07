using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Validators.Formatters
{
    public enum Formatter
    {
        None,
        Hyphens,
        HyphensAndWhiteSpaces,
        WitheSpaces,
    }


    public static class PostcodeFormatter
    {
        private static readonly Dictionary<Formatter, string> _formatters = new Dictionary<Formatter, string>()
        {
            { Formatter.None, string.Empty },
            { Formatter.WitheSpaces, @"\s+" },
            { Formatter.Hyphens,  @"\-+" },
            { Formatter.HyphensAndWhiteSpaces,  @"(\s+|\-+)"}
        };

        public static string Format(this string value)
            => Format(value, Formatter.None);

        public static string Format(this string value, Formatter formatter)
        {
            if (!_formatters.TryGetValue(formatter, out string replaceExpression))
                throw new ArgumentException(nameof(formatter));

            return Regex.Replace(value, replaceExpression, string.Empty);
        }
    }
}
