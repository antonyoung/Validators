namespace Validators.Interfaces
{
    public interface IIbanValidator
    {

        string Example { get; }

        string ErrorMessage { get;  }

        Countries Country { get; }

        bool IsValid { get; }

        bool Validate(string value, out string result);
    }
}
