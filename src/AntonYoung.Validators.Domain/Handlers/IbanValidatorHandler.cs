using AntonYoung.Validators.Domain.Abstractions.Exceptions;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Validators;
using AntonYoung.Validators.Iban.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AntonYoung.Validators.Domain.Handlers
{
    public interface IIbanValidatorHandler
    {
        Task<IbanValidationResponse> HandleAsync(IbanValidationRequest request, CancellationToken cancellationToken);
    }

    public class IbanValidatorHandler 
        : IIbanValidatorHandler
    {
        private readonly ILogger<IbanValidatorHandler> _logger;
        private readonly IIbanRequestValidator _requestValidator;
        private readonly IIbanValidator _ibanValidator;

        public IbanValidatorHandler(
            ILogger<IbanValidatorHandler> logger,
            IIbanRequestValidator requestValidator,
            IIbanValidator validator)
        {
            _logger = logger ?? NullLogger<IbanValidatorHandler>.Instance;
            _requestValidator = requestValidator;
            _ibanValidator = validator;
        }

        public async Task<IbanValidationResponse> HandleAsync(IbanValidationRequest request, CancellationToken cancellationToken)
        {
            var errors = await _requestValidator.ValidateAsync(request);

            if (errors.Any())
                throw new RequestException($"{nameof(PostalcodeValidaionRequest)} is not valid.", errors);

            _ibanValidator.TryValidate
            (
                request.Value,
                request.Formatter,
                request.Replace,
                out string result
            );

            return await Task.FromResult(
                new IbanValidationResponse
                {
                    CheckDigits = _ibanValidator.CheckDigits,
                    AccountType = _ibanValidator.AccountType,
                    AccountNumber = _ibanValidator.AccountNumber,
                    Country = _ibanValidator.Country,
                    ErrorMessage = _ibanValidator.ErrorMessage,
                    Result = result,
                    IsValid = _ibanValidator.IsValid,
                    NationalBankCode = _ibanValidator.NationalBankCode,
                    NationalBranchCode = _ibanValidator.NationalBranchCode,
                    NationalCheckDigit = _ibanValidator.NationalCheckDigit
                });
        }
    }
}