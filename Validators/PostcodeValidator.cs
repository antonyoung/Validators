using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Validators.Interfaces;


namespace Validators
{
    /// <summary>
    ///     used as all business logic behind postal codes of European countries ( with a few exceptions, like Finland as example. )
    ///     validates and formats the postal code according to the selected country.
    /// </summary>
    public class PostcodeValidator
        : IPostcodeValidator
    {

        #region public properties


        /// <summary>
        ///     used as a postal code example of the set country.  
        /// </summary>
        public string Example { get => _logic.Example; }


        /// <summary>
        ///     used as message, in case the postal code is not valid.  
        ///     is empty, when postal code is valid for the set country.
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
                    ErrorMessage = ErrorMessage = $"Postal code \"{_input}\" is not valid. Example \"{Example}\".";

                _isValid = value;
            }
        }

        #endregion


        #region private declarations

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
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{3}$)",
                    Example = "1234"
                }
            },
            { Countries.Belgium,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{3}$)",
                    Example = "1234"
                }
            },
            { Countries.Bulgaria,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{3}$)",
                    Example = "1234"
                }
            },
            { Countries.Croatia,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits>",
                    RegexPattern = "^(?<prefix>(?i)HR-)?(?<digits>[1-9][0-9]{4}$)",
                    Prefix = "HR-",
                    Example = "HR-12345"
                }
            },
            { Countries.Cyprus,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{3}$)",
                    Example = "1234"
                }
            },
            { Countries.Czechia,
                new CountryLogic {
                    DisplayFormat = "<digits1> <digits2>",
                    RegexPattern = @"(?<digits1>^[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)",
                    Example = "123 45"
                }
            },
            { Countries.Denmark,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{3}$)",
                    Example = "1234"
                }
            },
            { Countries.Estonia,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{4}$)",
                    Example = "12345"
                }
            },
            // Finland: The postal code must be preceded by ‘FI-’ = default (or by ‘AX-’ for the Åland Islands) 
            { Countries.Finland,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits>",
                    RegexPattern = "^(?<prefix>(?i)(FI-)|(AX-))?(?<digits>[1-9][0-9]{4}$)",
                    Prefix = "FI-",
                    Example = "FI-12345"
                }
            },
            { Countries.France,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{4}$)",
                    Example = "12345"
                }
            },
            { Countries.Germany,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{4}$)",
                    Example = "12345"
                }
            },
            { Countries.Greece,
                new CountryLogic {
                    DisplayFormat = "<digits1> <digits2>",
                    RegexPattern = @"(?<digits1>^[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)",
                    Example = "123 45"
                }
            },
            { Countries.Hungary,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{3}$)",
                    Example = "1234"
                }
            },
             { Countries.Italy,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{4}$)",
                    Example = "12345"
                }
            },
            // Ireland: not sure how correct this is?
            { Countries.Ireland,
                new CountryLogic {
                    DisplayFormat = "<alphanumeric1> <alphanumeric2>",
                    RegexPattern = @"(?<alphanumeric1>(?:^[AC-FHKNPRTV-Y][0-9]{2}|D6W))(?<whitespace>\s?)(?<alphanumeric2>[0-9AC-FHKNPRTV-Y]{4}$)",
                    Example = "D22 YD82"
                }
            },
            { Countries.Latvia,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits>",
                    RegexPattern = "^(?<prefix>(?i)LV-)?(?<digits>[1-9][0-9]{3}$)",
                    Prefix = "LV-",
                    Example = "LV-1234"
                }
            },
            { Countries.Lithuania,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits>",
                    RegexPattern = "^(?<prefix>(?i)LT-)?(?<digits>[1-9][0-9]{4}$)",
                    Prefix = "LT-",
                    Example = "LT-12345"
                }
            },
            { Countries.Luxembourg,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits>",
                    RegexPattern = "^(?<prefix>(?i)L-)?(?<digits>[1-9][0-9]{3}$)",
                    Prefix = "L-",
                    Example = "L-1234"
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
                    DisplayFormat = "<digits1>-<digits2>",
                    RegexPattern = @"(?<digits1>^[1-9][0-9]{1})(?<hyphen>-?)(?<digits2>[0-9]{3}$)",
                    Example = "12-345"
                }
            },
            { Countries.Portugal,
                new CountryLogic {
                    DisplayFormat = "<digits1>-<digits2>",
                    RegexPattern = @"(?<digits1>^[1-9][0-9]{3})(?<hyphen>-?)(?<digits2>[0-9]{3}$)",
                    Example = "1234-567"
                }
            },
            { Countries.Romania,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{5}$)",
                    Example = "123456"
                }
            },
            { Countries.Slovakia,
                new CountryLogic {
                    DisplayFormat = "<digits1> <digits2>",
                    RegexPattern = @"(?<digits1>^[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)",
                    Example = "123 45"
                }
            },
            { Countries.Slovenia,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits>",
                    RegexPattern = "^(?<prefix>(?i)SI-)?(?<digits>[1-9][0-9]{3}$)",
                    Prefix = "SI-",
                    Example = "SI-1234"
                }
            },
            { Countries.Spain,
                new CountryLogic {
                    DisplayFormat = "<digits>",
                    RegexPattern = "(?<digits>^[1-9][0-9]{4}$)",
                    Example = "12345"
                }
            },
            { Countries.Sweden,
                new CountryLogic {
                    DisplayFormat = "<prefix><digits1> <digits2>",
                    RegexPattern = @"^(?<prefix>(?i)SE-)?(?<digits1>[1-9][0-9]{2})(?<whitespace>\s?)(?<digits2>[0-9]{2}$)",
                    Prefix = "SE-",
                    Example = "SE-123 45"
                }
            },
            // United Kingdom: not sure how correct this is?
            { Countries.UnitedKingdom,
                new CountryLogic {
                    DisplayFormat = "<alphanumeric1> <alphanumeric2>",
                    RegexPattern = @"^(?<alphanumeric1>(([A-Z][0-9]{1,2})|(([A-Z][A-HJ-Y][0-9]{1,2})|(([A-Z][0-9][A-Z])|([A-Z][A-HJ-Y][0-9]?[A-Z])))))(?<whitespace>\s?)(?<alphanumeric2>[0-9][A-Z]{2}$)",
                    Example = "DN55 1PT"
                }
            }
         };

        private CountryLogic _logic;

        private string _input;
        private bool _isValid;

        #endregion



        /// <summary>
        ///     validates and formats postcode with default country The Netherlands.
        /// </summary>
        /// <param name="value">
        ///     used as the postcode to be validated with the default country The Netherlands and formatted as default. 
        /// </param>
        /// <param name="result">
        ///     used as the result postcode 
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postcode.
        /// </returns>
        public bool TryParse(string value, out string result) 
            => TryParse(value, Countries.Netherlands, out result);


        /// <summary>
        ///     validates and formats postcode with provided country.
        /// </summary>
        /// <param name="value">
        ///     used as the postcode to be validated with the default country The Netherlands and formatted as default. 
        /// </param>
        /// <param name="country">
        ///     used as the country to be validated and formatted as default. 
        /// </param>
        /// <param name="result">
        ///     used as the result postcode 
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postcode.
        /// </returns>
        public bool TryParse(string value, Countries country, out string result) 
            => TryParse(value, country, RemoveFormatter.Default, out result);


        /// <summary>
        ///     validates and formats postcode with provided country.
        /// </summary>
        /// <param name="value">
        ///     used as the postcode to be validated with the country and how it has to formatted. 
        /// </param>
        /// <param name="country">
        ///     used as the country to be validated and formatted as default. 
        /// </param>
        /// <param name="formatter">
        ///     used as additional formatter, how to format.
        /// </param>
        /// <param name="result">
        ///     used as the result postcode 
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postcode.
        /// </returns>
        public bool TryParse(string value, Countries country, RemoveFormatter formatter, out string result)
        { 
            _input = result = value.Trim();
            
            if (!_countries.TryGetValue(country, out _logic))
                throw new ArgumentException(nameof(country));

            var match = Regex.Match(_input, _logic.RegexPattern);

            IsValid = match.Success;

            if (!_isValid) return false;

            result = Format(match, formatter);

            return true;
        }


        /// <summary>
        ///     formats the postcode to the set country and the set formatter.
        /// </summary>
        /// <param name="match">
        ///     used as the match of the used internal regular expression.
        /// </param> 
        /// <param name="formatter">
        ///     used as the formatter how to format the result.
        /// </param>
        /// <returns>
        ///     postcode with provide formatter. 
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
