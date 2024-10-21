using AntonYoung.Validators.Console.Mappers;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;
using AntonYoung.Validators.Domain.Handlers;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Services
{
    public interface IValidatorService
    {
        Task ValidateAsync(ValidatorModel model);
    }

    public class ValidatorService(
        ILogger<ValidatorService> logger,
        IIbanValidatorHandler ibanValidatorHandler,
        IPostalcodeValidatorHandler postalcodeValidatorHandler,
        IPostalcodeRequestMapper postalcodeRequestMapper,
        IIbanRequestMapper ibanRequestMapper,
        IConsoleWriter writer)
        : IValidatorService
    {
        public async Task ValidateAsync(ValidatorModel model)
        {
            if (Enums.Applications.Iban == model.Application)
            {
                var request = await ibanRequestMapper
                    .MapAsync(model);

                var result = await ibanValidatorHandler
                    .HandleAsync(request, cancellationToken: default);

                await writer
                    .WriteAsync(result);
            }

            if (Enums.Applications.Post == model.Application)
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