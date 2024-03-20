using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Abstractions.Requests;

namespace AntonYoung.Validators.Domain.Validators
{
    public interface IIbanRequestValidator
    {
        Task<IEnumerable<string>> ValidateAsync(IbanValidationRequest request);
    }

    public class IbanRequestValidator 
        : IIbanRequestValidator
    {
        private readonly IList<string> _errorMessages;

        public IbanRequestValidator()
            => _errorMessages = new List<string>();

        public async Task<IEnumerable<string>> ValidateAsync(IbanValidationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Value))
                _errorMessages.Add("Value is obliged, to be able to do a iban validation.");

            if (!Enum.IsDefined(typeof(Formatters), request.Formatter.ToString()))
                _errorMessages.Add($"{request.Formatter} is not supported as formatter.");

            return await Task.FromResult(_errorMessages);
        }
    }
}