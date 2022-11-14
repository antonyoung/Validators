﻿using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Iban.Infrastructure;
using System;

namespace AntonYoung.Validators.Iban.Tests.TestModels
{
    public class IbanTestModel : IIbanValidator
    {
        public string Value { get; set; }

        public string AccountNumber { get; set; }
        
        public byte AccountType { get; set; }
        
        public byte CheckDigits { get; set; }

        public Countries Country { get; set; }

        public string Example { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsValid { get; set; }

        public string NationalBankCode { get; set; }

        public string NationalBranchCode { get; set; }

        public byte? NationalCheckDigit { get; set; }

        public bool TryValidate(string value, out string result) 
            => throw new NotImplementedException();

        public bool TryValidate(string value, Formatters formatter, out string result)
            => throw new NotImplementedException();

        public bool TryValidate(string value, Formatters formatter, string replace, out string result)
            => throw new NotImplementedException();
    }
}