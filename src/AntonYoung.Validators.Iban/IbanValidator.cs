using AntonYoung.Validators.Abstractions.Constants;
using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Abstractions.Extensions;
using AntonYoung.Validators.Iban.Constants;
using AntonYoung.Validators.Iban.Extensions;
using AntonYoung.Validators.Iban.Indexers;
using AntonYoung.Validators.Iban.Infrastructure;
using AntonYoung.Validators.Iban.Models;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AntonYoung.Validators.Iban
{
    /// <summary>
    ///     used as all business logic behind iban of European countries.
    ///     validates and formats the iban according to the iban country.
    ///     also retrieves the account number, bank code, branch code, check digits, country and national check digit of the iban value, if any?
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
        ///     used as internal logic of the current Regular Expression Match.
        /// </summary>
        private Match _match;

        /// <summary>
        ///     used as constructor to initiliaze the class with the internal business rules of all internal iban.
        /// </summary>
        public IbanValidator(IIbanModel model) => _model = model;

        /// <summary>
        ///     used as the account number of an iban value.
        /// </summary>
        public string AccountNumber => GroupValues(GroupNames.Account);

        /// <summary>
        ///     used as the account type of an iban value <see cref="Countries.Bulgaria"/> as as exception using this property.
        /// </summary>
        public byte? AccountType => byte.TryParse(GroupValues(GroupNames.AccountType), out byte result) ? (byte?)result : null;

        /// <summary>
        ///     used as the international check digits of the iban value. 
        /// </summary>
        public byte? CheckDigits => byte.TryParse(GroupValues(GroupNames.CheckDigits), out byte result) ? (byte?)result : null;      
        
        /// <summary>
        ///     used as the country of the iban value. 
        /// </summary>
        public Countries Country => _logic.Country;

        /// <summary>
        ///     used as an iban example of a country.
        /// </summary>
        public string Example { get => _logic.Example; }

        /// <summary>
        ///     used as error message in case of <seealso cref="IsValid"/> = false;
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        ///     used to validate given iban.
        ///     when false an <seealso cref="ErrorMessage"/> will be given.
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     used as bank code, could contain alpha numeric values.
        /// </summary>
        public string NationalBankCode => GroupValues(GroupNames.Bank);

        /// <summary>
        ///     used as the branch code of an iban value, if any?
        /// </summary>
        public string NationalBranchCode => GroupValues(GroupNames.Branch);

        /// <summary>
        ///     used as the check digit of an iban value, if any?
        /// </summary>
        public byte? NationalCheckDigit => byte.TryParse(GroupValues(GroupNames.NationalCheckDigit), out byte result) ? (byte?)result : null;

        /// <summary>
        ///     used as to validate an iban value
        /// </summary>
        /// <param name="value">
        ///     used as the iban value to be validated.
        /// </param>
        /// <param name="result">
        ///     used as out value of the formatted iban value <see cref="IsValid"/> = true
        ///     else the provided value is used as result.
        /// </param>
        /// <returns>
        ///     <see cref="IsValid"/> is a valid iban or not? With <seealso cref="Formatters"/> as default <see cref="Formatters.None"/>
        /// </returns>
        public bool TryValidate(string value, out string result)
            => TryValidate(value, Formatters.None, out result);

        /// <summary>
        ///     used as to validate an iban value
        /// </summary>
        /// <param name="value">
        ///     used as the iban value to be validated.
        /// </param>
        /// <param name="formatter">
        ///     usead as the formatter that has to be used.
        /// </param>
        /// <param name="result">
        ///     used as out value of the formatted iban value <see cref="IsValid"/> = true
        ///     else the provided value is used as result.
        /// </param>
        /// <returns>
        ///     <see cref="IsValid"/> is a valid iban or not? With default replace value <see cref="string.Empty"/> used by given <seealso cref="Formatters"/>
        /// </returns>
        public bool TryValidate(string value, Formatters formatter, out string result)
            => TryValidate(value, formatter, string.Empty, out result);

        /// <summary>
        ///     used as to validate an iban value
        /// </summary>
        /// <param name="value">
        ///     used as the iban value to be validated.
        /// </param>
        /// <param name="formatter">
        ///     usead as the formatter that has to be used.
        /// </param>
        /// <param name="replace">
        ///     usead as custom replace value to be used with given formatter.
        /// </param>
        /// <param name="result">
        ///     used as out value of the formatted iban value <see cref="IsValid"/> = true
        ///     formatted with <paramref name="formatter"/> and <paramref name="replace"/> custom value to be used with the formatter.
        ///     else the provided value is used as result.
        /// </param>
        /// <returns>
        ///     <see cref="IsValid"/> is a valid iban or not?
        /// </returns>
        public bool TryValidate(string value, Formatters formatter, string replace, out string result)
        {
            result = value.Trim().ToUpperInvariant()
                ?? throw new ArgumentException(nameof(value));

            //=> use TwoLetterISORegionName from iban value as key
            if (!_model.Rules.TryGetValue(result.Substring(0, 2).ToUpperInvariant(), out _logic))
                throw new ArgumentException($"No matching country found for {result.Substring(0, 2).ToUpperInvariant()}.");

            _match = Regex.Match(result, _logic.RegexPattern);

            if (!IsMatch(result)) return false;

            IsValid = IsSanityValid();

            if (IsValid)
            {
                result = Format(formatter, replace);
            }
            else
            {
                ErrorMessage = $"Ïban value of \"{result}\" is not valid as sanity validation. Use as example \"{_logic.Example}\" for country {Country}.";
            }

            return IsValid;
        }

        /// <summary>
        ///     used as to validate <see cref="_match"/>
        /// </summary>
        /// <param name="value">
        ///     used as internal logic, to validate <see cref="_match"/> as length without wildcards.
        /// </param>
        /// <returns>
        ///     <see cref="bool"/> true, if success else an <see cref="ErrorMessage"/> will be given.
        /// </returns>
        private bool IsMatch(string value)
        {
            IsValid = _match.Success;

            if (!IsValid)
            {
                // todo: use and add formatter to replace wildcards or add wildcards.
                int length = value.Format(Formatters.WhiteSpaces).Length;

                ErrorMessage = length == _logic.Length
                    ? $"Iban value of \"{value}\" is not valid. Use as example \"{_logic.Example}\" for country {Country}."
                    : $"Length {length} of given Iban is not valid. It should be an Length of {_logic.Length} for country {Country}. Use as example \"{Example}\".";
            }

            return IsValid;
        }

        /// <summary>
        ///     used to validate as sanity check, based on modules 97 created by <see cref="CreateSanityIndexer()"/>
        /// </summary>
        /// <returns>
        ///     <see cref="bool"/> depending if sanity is valid or not.
        /// </returns>
        private bool IsSanityValid()
        {
            double checkSum = 0;
            GenericIndexer<byte> sanityIndexer = CreateSanityIndexer();

            //=> research: System.ReadOnlySpan<char> => net core 3.0 new implementation?
            foreach (byte value in sanityIndexer)
            {
                //=> sanity check, current value * 10 + value modulus of 97.
                checkSum *= 10;
                checkSum += value;
                checkSum %= 97;
            }

            return checkSum == 1;

            //=> TODO: As generator iban values
            //=> Subtract the remainder from 98, and use the result for the two check digits. If the result is a single digit number, pad it with a leading 0 to make a two-digit number.
        }

        /// <summary>
        ///     used as the complete sanity check <see cref="GenericIndexer{T}"/> for regular expression <see cref="_match"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     throws this exception, when it can not convert <paramref name="match"/> as an <see cref="int"/>.
        /// </exception>
        /// <returns>
        ///     <see cref="GenericIndexer{byte}"/> to be used for the sanity check.
        /// </returns>
        private GenericIndexer<byte> CreateSanityIndexer()
        {
            //=> internal logic how we have to format the sanity check
            var formatAsNumbers = _logic.SanityFormat;
            
            //=> formats sanity, based on regular expression group.Names and their values.
            foreach (Group group in _match.Groups)
            {
                //=> TODO: clean up if statements.
                if (group.Name.Equals(GroupNames.Country, StringComparison.OrdinalIgnoreCase))
                {
                    formatAsNumbers = formatAsNumbers.Replace(string.Format(Replaces.Default, group.Name)
                        , group.Value.ToUpperInvariant().ToCharArray().CharAsInt().ToString());

                    continue;
                }

                if (group.Name.Equals(GroupNames.Bank, StringComparison.OrdinalIgnoreCase))
                    formatAsNumbers = formatAsNumbers.Replace(string.Format(Replaces.Default, group.Name)
                         , group.Value.ToUpperInvariant().ToCharArray().CharAsInt().ToString());

                if (group.Name.Equals(GroupNames.NationalCheckDigit, StringComparison.OrdinalIgnoreCase))
                    formatAsNumbers = formatAsNumbers.Replace(string.Format(Replaces.Default, group.Name)
                        , group.Value.ToUpperInvariant().ToCharArray().CharAsInt().ToString());

                if (group.Name.Equals($"{GroupNames.Account}4", StringComparison.OrdinalIgnoreCase))
                    formatAsNumbers = formatAsNumbers.Replace(string.Format(Replaces.Default, group.Name)
                         , group.Value.ToUpperInvariant().ToCharArray().CharAsInt().ToString());

                if (!string.IsNullOrWhiteSpace(group.Value))
                    formatAsNumbers = formatAsNumbers.Replace(string.Format(Replaces.Default, group.Name), group.Value);
            }

            //=> validate sanity is a number
            if (!double.TryParse(formatAsNumbers, out double sanityNumber))
                throw new ArgumentOutOfRangeException(nameof(sanityNumber));

            //=> reindex every didget [0- 9] as a byte
            return new GenericIndexer<byte>(formatAsNumbers
                .ToCharArray()
                .Select(value => { return byte.Parse(value.ToString()); }));
        }

        /// <summary>
        ///     used as how to format the iban, based on <seealso cref="_logic.DisplayFormat"/>.
        /// </summary>
        /// <param name="formatter">
        ///     used as <see cref="Formatters"/> to be used as replace wildcard ( default: <see cref="Formatters.None"/> )
        /// </param>
        /// <param name="replace">
        ///     used as given formatter, as replace value ( default: <see cref="string.Empty"/> )
        /// </param>
        /// <returns>
        ///     formatted value.
        /// </returns>
        private string Format(Formatters formatter, string replace)
        {
            string result = _logic.DisplayFormat;

            foreach (Group group in _match.Groups)
            {
                if (string.IsNullOrWhiteSpace(group.Value)) { continue; } 

                result = result.Replace(string.Format("<{0}>", group.Name), group.Value);
            }

            return result.ToUpperInvariant().Format(formatter, replace);
        }

        /// <summary>
        ///     used to append all values in given <paramref name="groupName"/> from the regular expression <see cref="_match"/>
        /// </summary>
        /// <param name="groupName">
        ///     used as the group name to be used.
        /// </param>
        /// <returns>
        ///     the appended values of <paramref name="groupName"/>
        ///     null, if nothing has been found.
        /// </returns>
        private string GroupValues(string groupName)
        {
            var append = new StringBuilder();
            var groups = _match.Groups
                .Where(_ => _.Name.Contains(groupName, StringComparison.OrdinalIgnoreCase));

            foreach (var group in groups)
                append.Append(group.Value);
            
            return append.Length == 0 ? null : append.ToString();
        }
    }
}