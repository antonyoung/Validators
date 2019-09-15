namespace Validators.Interfaces
{
    public interface IIbanValidator
    {
        
        string ErrorMessage { get;  }

        Countries Country { get; }

        string SwiftCode { get; }

        int AccountNumber { get; }

        bool IsValid { get; }

        bool Validate(string value, out string result);

        //int CharAsInt(char value);

        //int CharAsInt(char[] value);

    }
}
