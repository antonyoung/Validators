using AntonYoung.Validators.Console.Client.Enums;
using AntonYoung.Validators.Console.Client.Models;
using AntonYoung.Validators.Postalcode;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Client.Handlers
{
    internal class PostalcodeValidateHandler : IValidateHandler
    {
        private readonly ILogger<PostalcodeValidateHandler> _logger;
        private readonly PostalcodeValidator _validator;

        public PostalcodeValidateHandler(
            ILogger<PostalcodeValidateHandler> logger,
            PostalcodeValidator validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public Task HandleAsync(ValidatorModel model)
        {
            _validator.TryValidate
            (
                model.Value, 
                model.Country, 
                model.Formatter, 
                model.Replace, 
                out var result
            );

            _logger.LogInformation("{result}", result);
            _logger.LogInformation("{ErrorMessage}", _validator.ErrorMessage);
            _logger.LogInformation("{Example}", _validator.Example);
            _logger.LogInformation("{IsValid}", _validator.IsValid);

            return Task.CompletedTask;
        }

        public bool IsValidateHandler(Applications application)
        {
            return application == Applications.PostalCode;
        }
    }
}
