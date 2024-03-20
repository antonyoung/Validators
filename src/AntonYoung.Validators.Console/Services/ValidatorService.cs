using AntonYoung.Validators.Console.Mappers;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Handlers;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Services
{
    internal interface IValidatorService
    {
        Task ValidateAsync(ValidatorModel model);
    }

    internal class ValidatorService
        : IValidatorService
    {
        private readonly ILogger<ValidatorService> _logger;
        private readonly IIbanValidatorHandler _ibanValidatorHandler;
        private readonly IPostalcodeValidatorHandler _postalcodeValidatorHandler;
        private readonly IIbanRequestMapper _ibanRequestMapper;
        private readonly IPostalcodeRequestMapper _postalcodeRequestMapper;
        private readonly IConsoleWritter _writter;

        public ValidatorService (
            ILogger<ValidatorService> logger, 
            IIbanValidatorHandler ibanValidatorHandler,
            IPostalcodeValidatorHandler postalcodeValidatorHandler,
            IPostalcodeRequestMapper postalcodeRequestMapper,
            IIbanRequestMapper ibanRequestMapper,
            IConsoleWritter writter)
        {
            _logger = logger;
            _postalcodeValidatorHandler = postalcodeValidatorHandler;
            _ibanValidatorHandler = ibanValidatorHandler;
            _ibanRequestMapper = ibanRequestMapper;
            _postalcodeRequestMapper = postalcodeRequestMapper;
            _writter = writter;
        }

        public async Task ValidateAsync(ValidatorModel model)
        {
            if (model.Application == Enums.Applications.Iban)
            {
                var result = await ValidateIbanAsync(model);

                await _writter.WriteAsync(result);
            }

            if (model.Application == Enums.Applications.Post)
            {
                var result = await ValidatePostalcodeAsync(model);

                await _writter.WriteAsync(result);
            }
        }

        private async Task<IbanValidationResponse> ValidateIbanAsync(ValidatorModel model)
        {
            var request = await _ibanRequestMapper
                .MapAsync(model);

            return await _ibanValidatorHandler
                .HandleAsync(request, cancellationToken: default);
        }

        private async Task<PostalcodeValidationResponse> ValidatePostalcodeAsync(ValidatorModel model)
        {
            var request = await _postalcodeRequestMapper
                .MapAsync(model);

            return await _postalcodeValidatorHandler
                .HandleAsync(request, cancellationToken: default);
        }
    }
}