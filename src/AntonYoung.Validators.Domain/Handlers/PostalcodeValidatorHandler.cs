using AntonYoung.Validators.Domain.Abstractions.Exceptions;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Mappers;
using AntonYoung.Validators.Domain.Validators;
using AntonYoung.Validators.Postalcode.Infrastructure;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Domain.Handlers
{
    public interface IPostalcodeValidatorHandler
    {
        Task<PostalcodeValidationResponse> HandleAsync(PostalcodeValidaionRequest request, CancellationToken cancellationToken);
    }

    public class PostalcodeValidatorHandler 
        : IPostalcodeValidatorHandler
    {
        private readonly ILogger<PostalcodeValidatorHandler> _logger;
        private readonly IPostalcodeRequestValidator _requestValidator;
        private readonly IPostalcodeValidator _postalcodeValidator;
        private readonly ICountryMapper _mapper;

        public PostalcodeValidatorHandler(
            ILogger<PostalcodeValidatorHandler> logger,
            IPostalcodeRequestValidator requestValidator,
            IPostalcodeValidator validator,
            ICountryMapper mapper) 
        { 
            _logger = logger;
            _requestValidator = requestValidator;
            _postalcodeValidator = validator;
            _mapper = mapper;
        }

        public async Task<PostalcodeValidationResponse> HandleAsync(PostalcodeValidaionRequest request, CancellationToken cancellationToken)
        {
            var country = await _mapper.MapAsync(request.Country);

            var errors = await _requestValidator.ValidateAsync(request, country);

            if (errors.Any())
                throw new RequestException($"{nameof(PostalcodeValidaionRequest)} is not valid.", errors);

            _postalcodeValidator.TryValidate
            (
                request.Value,
                country,
                request.Formatter,
                request.Replace,
                out string result
            );

            return await Task.FromResult(
                new PostalcodeValidationResponse
                { 
                    Result = result,
                    IsValid = _postalcodeValidator.IsValid,
                    ErrorMessage = _postalcodeValidator.ErrorMessage,
                    Country = country
                });
        }
    }
}