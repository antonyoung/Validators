using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Validators.Interfaces;


namespace Validators
{

    /// <summary>
    ///     used as all business logic behind postcodes of European countries ( with the exceptions with preifix <see cref="Countries.Finland"/> )
    ///     validates and formats the postal code according to the selected country.
    /// </summary>
    public class PostcodeValidator
        : IPostcodeValidator
    {

        /// <summary>
        ///     used as a postcode example of the set country. ( Default = <see cref="Countries.Netherlands"/>  
        /// </summary>
        public string Example { get => _logic.Example; }


        /// <summary>
        ///     used as error message in case of <seealso cref="IsValid"/> = false;
        /// </summary>
        public string ErrorMessage { get; private set; }


        /// <summary>
        ///     used to validate given postal code against the set country.
        ///     when false an <seealso cref="ErrorMessage"/> will be given.
        /// </summary>
        public bool IsValid
        {
            get => _isValid;
            private set
            {
                if(value == false)
                   ErrorMessage = $"Postcode \"{_input}\" is not valid! Use as example \"{Example}\".";

                _isValid = value;
            }
        }


        #region private declarations


        // => internal logic: Of the counrty that has been selectd.
        private CountryLogic _logic;

        // => the input postcode that has to be validated and to be formatted.
        private string _input;

        // internal logic => Default = false; 
        private bool _isValid = false;
        
        // constants => used 2 => n times.
        private const string EXAMPLE_4_DIGITS = "1234";
        private const string EXAMPLE_5_DIGITS = "12345";
        private const string EXAMPLE_5_DIGITS_WHITESPACE = "123 45";

        private const string FORMAT_ALPHANUMERIC = "<alphanumeric1> <alphanumeric2>";
        private const string FORMAT_DIGITS = "<digits>"; 
        private const string FORMAT_DIGITS_HYPHEN = "<digits1>-<digits2>";
        private const string FORMAT_DIGITS_WHITESPACE = "<digits1> <digits2>";
        private const string FORMAT_PREFIX = "<prefix><digits>";

        private const string PREFIX_CROATIA = "HR-";
        private const string PREFIX_FINLAND = "FI-";
        private const string PREFIX_LATVIA = "LV-";
        private const string PREFIX_LITHUANIA = "LT-";
        private const string PREFIX_LUXEMBOURG = "L-";
        private const string PREFIX_SLOVENIA = "SI-";
        private const string PREFIX_SWEDEN = "SE-";

        private const string REPLACE_PREFIX = "<REPLACE_PREFIX>";

        private const string REGEX_4_DIGITS = "(?<digits>^[1-9][0-9]{3}$)";
        private const string REGEX_4_DIGITS_PREFIX = "^(?<prefix>(?i)<REPLACE_PREFIX>)?(?<digits>[1-9][0-9]{3}$)";
        private const string REGEX_5_DIGITS = "(?<digits>^[1-9][0-9]{4}$)";
        private const string REGEX_5_DIGITS_PREFIX = "^(?<prefix>(?i)<REPLACE_PREFIX>)?(?<digits>[1-9][0-9]{4}$)";
        private const string REGEX_5_DIGITS_WHITESPACE = @"(?<digits1>^[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)";


        /// <summary>
        ///     used as internal business logic, to differentiate between countries. 
        /// </summary>
        private struct CountryLogic
        {
            public string RegexPattern { get; set; }
            public string DisplayFormat { get; set; }
            public string Prefix { get; set; }
            public string Example { get; set; }
        }


        /// <summary>
        ///     sets all countries available and their internal business logic.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     throws ArgumentException, in case country is not found in current Dictionary, 
        /// </exception> 
        private readonly Dictionary<Countries, CountryLogic> _countries = new Dictionary<Countries, CountryLogic>()
        {
            { Countries.Austria,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_4_DIGITS,
                    Example = EXAMPLE_4_DIGITS
                }
            },
            { Countries.Belgium,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_4_DIGITS,
                    Example = EXAMPLE_4_DIGITS
                }
            },
            { Countries.Bulgaria,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_4_DIGITS,
                    Example = EXAMPLE_4_DIGITS
                }
            },
            { Countries.Croatia,
                new CountryLogic {
                    DisplayFormat = FORMAT_PREFIX,
                    RegexPattern = REGEX_5_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_CROATIA),
                    Prefix = PREFIX_CROATIA,
                    Example = $"{PREFIX_CROATIA}{EXAMPLE_5_DIGITS}"
                }
            },
            { Countries.Cyprus,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_4_DIGITS,
                    Example = EXAMPLE_4_DIGITS
                }
            },
            { Countries.Czechia,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS_WHITESPACE,
                    RegexPattern = REGEX_5_DIGITS_WHITESPACE,
                    Example = EXAMPLE_5_DIGITS_WHITESPACE
                }
            },
            { Countries.Denmark,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_4_DIGITS,
                    Example = EXAMPLE_4_DIGITS
                }
            },
            { Countries.Estonia,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_5_DIGITS,
                    Example = EXAMPLE_5_DIGITS
                }
            },
            // Finland: The postal code must be preceded by ‘FI-’ = default (or by ‘AX-’ for the Åland Islands) 
            { Countries.Finland,
                new CountryLogic {
                    DisplayFormat = FORMAT_PREFIX,
                    RegexPattern = "^(?<prefix>(?i)(FI-)|(AX-))?(?<digits>[1-9][0-9]{4}$)",
                    Prefix = PREFIX_FINLAND,
                    Example = $"{PREFIX_FINLAND}{EXAMPLE_5_DIGITS}"
                }
            },
            { Countries.France,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_5_DIGITS,
                    Example = EXAMPLE_5_DIGITS
                }
            },
            { Countries.Germany,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_5_DIGITS,
                    Example = EXAMPLE_5_DIGITS
                }
            },
            { Countries.Greece,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS_WHITESPACE,
                    RegexPattern = REGEX_5_DIGITS_WHITESPACE,
                    Example = EXAMPLE_5_DIGITS_WHITESPACE
                }
            },
            { Countries.Hungary,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_4_DIGITS,
                    Example = EXAMPLE_4_DIGITS
                }
            },
             { Countries.Italy,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_5_DIGITS,
                    Example = EXAMPLE_5_DIGITS
                }
            },
            // Ireland: not sure how correct this is?
            { Countries.Ireland,
                new CountryLogic {
                    DisplayFormat = FORMAT_ALPHANUMERIC,
                    RegexPattern = @"(?<alphanumeric1>(?:^[AC-FHKNPRTV-Y][0-9]{2}|D6W))(?<whitespace>\s?)(?<alphanumeric2>[0-9AC-FHKNPRTV-Y]{4}$)",
                    Example = "D22 YD82"
                }
            },
            { Countries.Latvia,
                new CountryLogic {
                    DisplayFormat = FORMAT_PREFIX,
                    RegexPattern = REGEX_4_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_LATVIA),
                    Prefix = PREFIX_LATVIA,
                    Example = $"{PREFIX_LATVIA}{EXAMPLE_4_DIGITS}"
                }
            },
            { Countries.Lithuania,
                new CountryLogic {
                    DisplayFormat = FORMAT_PREFIX,
                    RegexPattern = REGEX_5_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_LITHUANIA),
                    Prefix = PREFIX_LITHUANIA,
                    Example = $"{PREFIX_LITHUANIA}{EXAMPLE_5_DIGITS}"
                }
            },
            { Countries.Luxembourg,
                new CountryLogic {
                    DisplayFormat = FORMAT_PREFIX,
                    RegexPattern = REGEX_4_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_LUXEMBOURG),
                    Prefix = PREFIX_LUXEMBOURG,
                    Example = $"{PREFIX_LUXEMBOURG}{EXAMPLE_4_DIGITS}"
                }
            },
            { Countries.Malta,
                new CountryLogic {
                    DisplayFormat = "<letters> <digits>",
                    RegexPattern =  @"(?<letters>^[A-Za-z]{3})(?<whitespace>\s?)(?<digits>[1-9][0-9]{3}$)",
                    Example = "ABC 1234"
                }
            },
            { Countries.Netherlands,
                new CountryLogic {
                    DisplayFormat = "<digits> <letters>",
                    RegexPattern =  @"(?<digits>^[1-9][0-9]{3})(?<whitespace>\s?)(?<letters>[A-Za-z]{2}$)",
                    Example = "1234 AB"
                }
            },
            { Countries.Poland,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS_HYPHEN,
                    RegexPattern = @"(?<digits1>^[1-9][0-9]{1})(?<hyphen>-?)(?<digits2>[0-9]{3}$)",
                    Example = "12-345"
                }
            },
            { Countries.Portugal,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS_HYPHEN,
                    RegexPattern = @"(?<digits1>^[1-9][0-9]{3})(?<hyphen>-?)(?<digits2>[0-9]{3}$)",
                    Example = "1234-567"
                }
            },
            { Countries.Romania,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = "(?<digits>^[1-9][0-9]{5}$)",
                    Example = "123456"
                }
            },
            { Countries.Slovakia,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS_WHITESPACE,
                    RegexPattern = REGEX_5_DIGITS_WHITESPACE,
                    Example = EXAMPLE_5_DIGITS_WHITESPACE
                }
            },
            { Countries.Slovenia,
                new CountryLogic {
                    DisplayFormat = FORMAT_PREFIX,
                    RegexPattern = REGEX_4_DIGITS_PREFIX.Replace(REPLACE_PREFIX, PREFIX_SLOVENIA),
                    Prefix = PREFIX_SLOVENIA,
                    Example = $"{PREFIX_SLOVENIA}{EXAMPLE_4_DIGITS}"
                }
            },
            { Countries.Spain,
                new CountryLogic {
                    DisplayFormat = FORMAT_DIGITS,
                    RegexPattern = REGEX_5_DIGITS,
                    Example = EXAMPLE_5_DIGITS
                }
            },
            { Countries.Sweden,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits1> <digits2>",
                    RegexPattern = @"^(?<prefix>(?i)SE-)?(?<digits1>[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)",
                    Prefix = PREFIX_SWEDEN,
                    Example = $"{PREFIX_SWEDEN}{EXAMPLE_5_DIGITS_WHITESPACE}"
                }
            },
            // United Kingdom: not sure how correct this is?
            { Countries.UnitedKingdom,
                new CountryLogic {
                    DisplayFormat = FORMAT_ALPHANUMERIC,
                    RegexPattern = @"^(?<alphanumeric1>(([A-Z][0-9]{1,2})|(([A-Z][A-HJ-Y][0-9]{1,2})|(([A-Z][0-9][A-Z])|([A-Z][A-HJ-Y][0-9]?[A-Z])))))(?<whitespace>\s?)(?<alphanumeric2>[0-9][A-Z]{2}$)",
                    Example = "DN55 1PT"
                }
            }
         };

        #endregion


        /// <summary>
        ///     validates and formats postcode with default country The Netherlands and default formatter as expected.
        /// </summary>
        /// <param name="value">
        ///     used as the postcode, that has to be validated with default country <see cref="Countries.Netherlands"/>. 
        /// </param>
        /// <param name="result">
        ///     used as <see cref="IsValid"/> <paramref name="value"/> with default country <see cref="Countries.Netherlands"/> formatted as the default formatter.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postcode.
        /// </returns>
        public bool TryParse(string value, out string result) 
            => TryParse(value, Countries.Netherlands, out result);


        /// <summary>
        ///     validates and formats postcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postcode, that has to be validated. 
        /// </param>
        /// <param name="country">
        ///     used as the country, that has to be validated.
        /// </param>
        /// <param name="result">
        ///     used as <see cref="IsValid"/> <paramref name="value"/> with <paramref name="country"/>` formatted with the default formatter.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postcode.
        /// </returns>
        public bool TryParse(string value, Countries country, out string result) 
            => TryParse(value, country, RemoveFormatter.Default, out result);


        /// <summary>
        ///     validates and formats postcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postcode, that has to be validated. 
        /// </param>
        /// <param name="country">
        ///     used as the country, that has to be validated.
        /// </param>
        /// <param name="formatter">
        ///     usead as the formatter that has to be used ( task: todo interfaced formatter. )
        /// </param>
        /// <param name="result">
        ///     used as postcode <see cref="IsValid"/> <paramref name="value"/> with <paramref name="country"/>` formatted with <paramref name="formatter"/>.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postcode.
        /// </returns>
        public bool TryParse(string value, Countries country, RemoveFormatter formatter, out string result)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));

            if (!_countries.TryGetValue(country, out _logic))
                throw new ArgumentException(nameof(country));

            _input = result = value.Trim();
            
            var match = Regex.Match(_input, _logic.RegexPattern);

            IsValid = match.Success;

            if (_isValid) result = Format(match, formatter);

            return _isValid;
        }


        /// <summary>
        ///     internal formatting logic, based on the groups that are in the match of whatever regular ecpression.
        /// </summary>
        /// <param name="match">
        ///     used as all groups that are matched in the used regular expression.
        /// </param> 
        /// <param name="formatter">
        ///     used as the formatter how to format result.
        /// </param>
        /// <returns>
        ///     the match result as poscode with given formatter. 
        /// </returns>
        private string Format(Match match, RemoveFormatter formatter)
        {
            string result = _logic.DisplayFormat;

            foreach (Group group in match.Groups)
            {
                if (group.Name.Equals("prefix", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(group.Value))
                    result = result.Replace(string.Format("<{0}>", group.Name), _logic.Prefix);

                if (!string.IsNullOrWhiteSpace(group.Value))
                    result = result.Replace(string.Format("<{0}>", group.Name), group.Value);
            }
            
            if (formatter != RemoveFormatter.Default)
            {
                if(formatter == RemoveFormatter.HyphensAndWhiteSpaces 
                    || formatter == RemoveFormatter.WitheSpaces)
                {
                    result = result.Replace(" ", string.Empty);
                }
                if (formatter == RemoveFormatter.HyphensAndWhiteSpaces
                    || formatter == RemoveFormatter.Hyphens)
                {
                    result = result.Replace("-", string.Empty);
                }
            }

            return result.ToUpperInvariant();
        }
    }
}
