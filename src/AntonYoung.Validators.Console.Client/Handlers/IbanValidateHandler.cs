using AntonYoung.Validators.Console.Client.Enums;
using AntonYoung.Validators.Console.Client.Models;
using AntonYoung.Validators.Iban;
using AntonYoung.Validators.Iban.Infrastructure;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Client.Handlers
{
    internal class IbanValidateHandler : IValidateHandler
    { 
        private readonly ILogger<IbanValidateHandler> _logger;
        private readonly IIbanValidator _validator;

        public IbanValidateHandler(
            ILogger<IbanValidateHandler> logger,
            IbanValidator validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public Task HandleAsync(ValidatorModel model)
        {
            _validator.TryValidate
            (
                model.Value, 
                model.Formatter, 
                model.Replace, 
                out string result
            );

            _logger.LogInformation("{result}", result);
            _logger.LogInformation("{AccountNumber}", _validator.AccountNumber);
            _logger.LogInformation("{CheckDigits}", _validator.CheckDigits);
            _logger.LogInformation("{Country}", _validator.Country);
            _logger.LogInformation("{ErrorMessage}", _validator.ErrorMessage);
            _logger.LogInformation("{Example}", _validator.Example);
            _logger.LogInformation("{IsValid}", _validator.IsValid);
            _logger.LogInformation("{NationalBankCode}", _validator.NationalBankCode);
            _logger.LogInformation("{NationalBranchCode}", _validator.NationalBranchCode);
            _logger.LogInformation("{NationalCheckDigit}", _validator.NationalCheckDigit);

            return Task.CompletedTask;
        }

        public bool IsValidateHandler(Applications application)
        {
            return application == Applications.Iban;
        }
    }
}
