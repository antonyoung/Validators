using AntonYoung.Validators.Console.Mappers;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;
using AntonYoung.Validators.Domain.Handlers;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Services
{
    internal interface IValidatorService
    {
        Task ValidateAsync(ValidatorModel model);
    }

    internal class ValidatorService(
        ILogger<ValidatorService> logger,
        IIbanValidatorHandler ibanValidatorHandler,
        IPostalcodeValidatorHandler postalcodeValidatorHandler,
        IPostalcodeRequestMapper postalcodeRequestMapper,
        IIbanRequestMapper ibanRequestMapper,
        IConsoleWriter writer)
                : IValidatorService
    {
        private readonly ILogger<ValidatorService> _logger = logger;

        public async Task ValidateAsync(ValidatorModel model)
        {
            if (model.Application == Enums.Applications.Iban)
            {
                var request = await ibanRequestMapper
                    .MapAsync(model);

                var result = await ibanValidatorHandler
                    .HandleAsync(request, cancellationToken: default);

                await writer
                    .WriteAsync(result);
            }

            if (model.Application == Enums.Applications.Post)
            {
                var request = await postalcodeRequestMapper
                    .MapAsync(model);

                var result = await postalcodeValidatorHandler
                    .HandleAsync(request, cancellationToken: default);

                await writer
                    .WriteAsync(result);
            }
        }
    }
}