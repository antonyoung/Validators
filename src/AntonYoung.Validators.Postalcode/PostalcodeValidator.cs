using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Abstractions.Extensions;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Models;
using System;
using System.Text.RegularExpressions;

namespace AntonYoung.Validators.Postalcode
{
    /// <summary>
    ///     used as all business logic behind postalcodes of European countries ( with the exceptions with preifix <see cref="Countries.Finland"/> )
    ///     validates and formats the postalcode according to the selected country.
    /// </summary>
    public class PostalcodeValidator : IPostalcodeValidator
    {
        /// <summary>
        ///     used as internal business rules of European postalcodes.
        /// </summary>
        private readonly IPostalcodeModel _model;

        /// <summary>
        ///     used as internal business rules of postalcode of selected country.
        /// </summary>
        private PostalcodeRuleSetModel _logic;

        /// <summary>
        ///     used as the internal postalcode, that has to be validated to the set country.
        /// </summary>
        private string _input;

        /// <summary>
        ///     used as internal business logic, if postalcode of selected country is valid.
        /// </summary>
        private bool _isValid;

        /// <summary>
        ///     used as constructor to initiliaze the class with the internal business rules of all internal postalcodes.
        /// </summary>
        public PostalcodeValidator() => _model = new PostalcodeModel();

        /// <summary>
        ///     used as a postalcode example of the set country. ( default = <see cref="Countries.Netherlands"/> )
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
                if (value == false)
                    ErrorMessage = ErrorMessage = $"Postal code \"{_input}\" is not valid. Use as example \"{Example}\".";

                _isValid = value;
            }
        }

        /// <summary>
        ///     validates and formats postalcode with default country The Netherlands and default formatter as expected.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated with default country <see cref="Countries.Netherlands"/>. 
        /// </param>
        /// <param name="result">
        ///     used as <see cref="IsValid"/> <paramref name="value"/> with default country <see cref="Countries.Netherlands"/> formatted as the default formatter.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postalcode.
        /// </returns>
        public bool TryValidate(string value, out string result) 
            => TryValidate(value, Countries.Netherlands, out result);

        /// <summary>
        ///     validates and formats postalcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated. 
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
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postalcode.
        /// </returns>
        public bool TryValidate(string value, Countries country, out string result) 
            => TryValidate(value, country, Formatters.None, out result);

        /// <summary>
        ///     validates and formats postalcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated. 
        /// </param>
        /// <param name="country">
        ///     used as the country, that has to be validated.
        /// </param>
        /// <param name="formatter">
        ///     usead as the formatter that has to be used.
        /// </param>
        /// <param name="result">
        ///     used as postalcode <see cref="IsValid"/> <paramref name="value"/> with <paramref name="country"/>` formatted with <paramref name="formatter"/>.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postalcode.
        /// </returns>
        public bool TryValidate(string value, Countries country, Formatters formatter, out string result)
            => TryValidate(value, country, formatter, string.Empty, out result);

        /// <summary>
        ///     validates and formats postalcode with provided country with default formatter as writen lanquage.
        /// </summary>
        /// <param name="value">
        ///     used as the postalcode, that has to be validated. 
        /// </param>
        /// <param name="country">
        ///     used as the country, that has to be validated.
        /// </param>
        /// <param name="formatter">
        ///     usead as the formatter that has to be used.
        /// </param>
        /// <param name="replace">
        ///     usead as custom replace value to be used with the given formatter.
        /// </param>
        /// <param name="result">
        ///     used as postalcode <see cref="IsValid"/> <paramref name="value"/> with <paramref name="country"/> 
        ///     formatted with <paramref name="formatter"/> and <paramref name="replace"/> custom value to be used with the formatter.
        /// </param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="RegexMatchTimeoutException"/>
        /// <returns>
        ///     <seealso cref="bool"/> is valid or not and as out the formatted postalcode.
        /// </returns>
        public bool TryValidate(string value, Countries country, Formatters formatter, string replace, out string result)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _input = result = value.Trim();

            if (!_model.Rules.TryGetValue(country, out _logic))
                throw new ArgumentException(nameof(country));

            var match = Regex.Match(_input, _logic.RegexPattern);

            IsValid = match.Success;

            if (_isValid) result = Format(match, formatter, replace);

            return _isValid;
        }

        /// <summary>
        ///     internal formatting logic, based on the groups that are in the match of whatever regular ecpression.
        /// </summary>
        /// <param name="match">
        ///     used as all groups that are matched in the used regular expression.
        /// </param> 
        /// <param name="formatter">
        ///     used as the formatter how to format the result.
        /// </param>
        /// <returns>
        ///     the formatted postalcode of the match result and with given formatter. 
        /// </returns>
        private string Format(Match match, Formatters formatter, string replace)
        {
            string result = _logic.DisplayFormat;

            foreach (Group group in match.Groups)
            {
                if (group.Name.Equals("prefix", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(group.Value))
                    result = result.Replace(string.Format("<{0}>", group.Name), _logic.Prefix);

                if (!string.IsNullOrWhiteSpace(group.Value))
                    result = result.Replace(string.Format("<{0}>", group.Name), group.Value);
            }

            return result.ToUpperInvariant().Format(formatter, replace);
        }
    }
}