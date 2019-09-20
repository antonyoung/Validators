namespace Validators.Interfaces
{
    public interface IIbanValidator
    {

        string AccountNumber { get; }

        byte CheckDidgets { get; }

        Countries Country { get; }

        string Example { get; }

        string ErrorMessage { get;  }

        bool IsValid { get; }
        
        string NationalBankCode { get; }

        string NationalBranchCode { get; }

        byte? NationalCheckDidget { get; }
        
        bool Validate(string value, out string result);
    }
}
