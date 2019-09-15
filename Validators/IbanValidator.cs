using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Validators.Indexers;
using Validators.Interfaces;
using Validators.Models;


namespace Validators
{

    /// <summary>
    /// 
    /// </summary>
    public class IbanValidator
        : IIbanValidator
    {
        private readonly IIbanModel _model;
        private IbanRuleSetModel _logic;

        private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
       
        public IbanValidator() => _model = new IbanModel();


        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public Countries Country => _logic.Country;


        public string SwiftCode => throw new NotImplementedException();

        public int AccountNumber => throw new NotImplementedException();


        /// <summary>
        /// 
        /// </summary>
        public bool IsValid
        {
            get; private set;
        }


        private int CharAsInt(char value)
        {
            var index = _alphabet.IndexOf(value);

            // char not found!
            if (index == -1)
                throw new ArgumentOutOfRangeException(nameof(value));

            // where A = 10, B = 11, ..., Z = 35
            return index += 10;
        }
         

        private int CharAsInt(char[] values)
        {
            var stringBuilder = new StringBuilder(values.Length * 2);

            foreach (var number in values)
                stringBuilder.Append(CharAsInt(number));

            if (!int.TryParse(stringBuilder.ToString(), out int result))
                throw new ArgumentException(nameof(values));

            return result;
        }


        private GenericIndexer<byte> CreateSanityIndexer(Match match)
        {
            var country = CharAsInt(match.Groups.Single(group
                => group.Name == "country").Value.ToCharArray());

            //if (match.Groups.Contains(item => item.Name == "name")
            //{
                var name = CharAsInt(match.Groups.SingleOrDefault(group
                    => group.Name == "name").Value.ToCharArray());
            //}

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

        private bool IsSanityValid(Match match)
        {
            double checkSum = 0;
            GenericIndexer<byte> sanityIndexer = CreateSanityIndexer(match);

            // RECEARCH: System.ReadOnlySpan<char> => net core 3.0 new implementation?
            foreach (byte value in sanityIndexer)
            {
                checkSum *= 10;
                checkSum += value;
                checkSum %= 97;
            }

            return checkSum == 1;
        }


        public bool Validate(string value, out string result)
        {
            result = value.Trim()
                ?? throw new ArgumentException(nameof(value));

            // use TwoLetterISORegionName as key
            if (!_model.Rules.TryGetValue(value.Substring(0, 2).ToUpperInvariant(), out _logic))
                throw new ArgumentException("no mathichint country found.");

            var match = Regex.Match(result, _logic.RegexPattern);

            IsValid = match.Success;

            if(IsValid)
            {
                IsValid = IsSanityValid(match);

                // todo: format match value;
            }          

            return IsValid;
        }

        //int IIbanValidator.CharAsInt(char value)
        //{
        //    throw new NotImplementedException();
        //}

        //int IIbanValidator.CharAsInt(char[] value)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
