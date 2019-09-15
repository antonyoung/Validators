﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Validators.Indexers;
using Validators.Interfaces;
using Validators.Models;


namespace Validators
{

    /// <summary>
    ///     used as all business logic behind iban of European countries.
    ///     validates and formats the iban according to the iban country.
    /// </summary>
    public class IbanValidator
        : IIbanValidator
    {

        /// <summary>
        ///     used as internal business rules of European iban.
        /// </summary>
        private readonly IIbanModel _model;


        /// <summary>
        ///      used as internal business rules of iban of selected country.
        /// </summary>
        private IbanRuleSetModel _logic;


        /// <summary>
        ///     used as internal business logic for sanity check, based on index.
        /// </summary>
        private const string _alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";


        /// <summary>
        ///         used as constructor to initiliaze the class with the internal business rules of all internal iban.
        /// </summary>
        public IbanValidator() => _model = new IbanModel();


        /// <summary>
        ///     used as an iban example of a country.
        /// </summary>
        public string Example { get => _logic.Example; }


        /// <summary>
        ///     used as error message in case of <seealso cref="IsValid"/> = false;
        /// </summary>
        public string ErrorMessage { get; private set; }


        /// <summary>
        ///     used as the country of the iban value. 
        /// </summary>
        public Countries Country => _logic.Country;


        /// <summary>
        ///     used to validate given iban.
        ///     when false an <seealso cref="ErrorMessage"/> will be given.
        /// </summary>
        public bool IsValid
        {
            get; private set;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        /// 
        /// </param>
        /// <param name="result">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public bool Validate(string value, out string result)
        {
            result = value.Trim()
                ?? throw new ArgumentException(nameof(value));

            // use TwoLetterISORegionName as key
            if (!_model.Rules.TryGetValue(value.Substring(0, 2).ToUpperInvariant(), out _logic))
                throw new ArgumentException("no matching country found.");

            var match = Regex.Match(result, _logic.RegexPattern);

            IsValid = match.Success;

            if (IsValid)
            {
                IsValid = IsSanityValid(match);

                // todo: format match value;
            }

            return IsValid;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="match">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        private bool IsSanityValid(Match match)
        {
            double checkSum = 0;
            GenericIndexer<byte> sanityIndexer = CreateSanityIndexer(match);

            // RESEARCH: System.ReadOnlySpan<char> => net core 3.0 new implementation?
            foreach (byte value in sanityIndexer)
            {
                checkSum *= 10;
                checkSum += value;
                checkSum %= 97;
            }

            return checkSum == 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="match">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        private GenericIndexer<byte> CreateSanityIndexer(Match match)
        {
            var country = CharAsInt(match.Groups.Single(group
                => group.Name == "country").Value.ToCharArray());

            int name = 0;
            if (match.Groups.Any(item => item.Name.Equals("name", StringComparison.OrdinalIgnoreCase)))
            {
                name = CharAsInt(match.Groups.SingleOrDefault(group
                    => group.Name == "name").Value.ToCharArray());
            }

            var formatAsNumbers = _logic.SanityFormat;

            foreach (Group group in match.Groups)
            {
                if (group.Name.Equals("country", StringComparison.OrdinalIgnoreCase))
                    formatAsNumbers = formatAsNumbers.Replace(string.Format("<{0}>", group.Name), country.ToString());

                if (group.Name.Equals("name", StringComparison.OrdinalIgnoreCase))
                    formatAsNumbers = formatAsNumbers.Replace(string.Format("<{0}>", group.Name), name.ToString());

                if (!string.IsNullOrWhiteSpace(group.Value))
                    formatAsNumbers = formatAsNumbers.Replace(string.Format("<{0}>", group.Name), group.Value);
            }

            if (!double.TryParse(formatAsNumbers, out double sanityNumber))
                throw new ArgumentOutOfRangeException(nameof(sanityNumber));

            return new GenericIndexer<byte>(
                formatAsNumbers.ToCharArray()
                    .Select(value => { return byte.Parse(value.ToString()); })
                );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        private int CharAsInt(char[] value)
        {
            var stringBuilder = new StringBuilder(value.Length * 2);

            foreach (var alphaNumber in value)
                stringBuilder.Append(CharAsInt(alphaNumber));

            if (!int.TryParse(stringBuilder.ToString(), out int result))
                throw new ArgumentException(nameof(value));

            return result;
        }


        /// <summary>
        ///     used as to convert a letter to an int.
        /// </summary>
        /// <param name="value">
        ///     used as the char, that has to be converted.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     in case <paramref name="value"/> has not been found.
        /// </exception>
        /// <returns>
        ///     int value of <paramref name="value"/> to be used for the sanity check.
        /// </returns>
        private int CharAsInt(char value)
        {
            var index = _alphabet.IndexOf(value);

            // => value not found!
            if (index == -1)
                throw new ArgumentOutOfRangeException(nameof(value));

            // => where A = 10, B = 11, ..., Z = 35
            return index;
        }
    }
}