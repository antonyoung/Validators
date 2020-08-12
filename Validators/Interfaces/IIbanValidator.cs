using Validators.Enums;

namespace Validators.Interfaces
{
    public interface IIbanValidator
    {
        string AccountNumber { get; }
            
        byte AccountType { get; }

        byte CheckDigits { get; }

        Countries Country { get; }

        string Example { get; }

        string ErrorMessage { get;  }

        bool IsValid { get; }
        
        string NationalBankCode { get; }

        string NationalBranchCode { get; }

        byte? NationalCheckDigit { get; }
        
        bool TryValidate(string value, out string result);
    }
}