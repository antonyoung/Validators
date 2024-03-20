using AntonYoung.Validators.Abstractions.Enums;

namespace AntonYoung.Validators.Domain.Abstractions.Responses
{
    public class IbanValidationResponse
    {
        public required string Result { get; set; }
        public bool IsValid { get; set; }
        public string? AccountNumber { get; set; }
        public byte? AccountType {  get; set; }
        public byte? CheckDigits { get; set; }
        public Countries? Country { get; set; }
        public string? ErrorMessage { get; set; }
        public string? NationalBankCode { get; set; }
        public string? NationalBranchCode { get; set; }
        public byte? NationalCheckDigit { get; set; }
    }
}