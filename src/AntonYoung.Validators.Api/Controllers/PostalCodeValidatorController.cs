using AntonYoung.Validators.Domain.Abstractions.Exceptions;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace AntonYoung.Validators.Api.Controllers
{
    [ApiController]
    [Route("validator/postalcode")]
    public class PostalCodeValidatorController 
        : ControllerBase
    {
        private readonly ILogger<PostalCodeValidatorController> _logger;
        private readonly IPostalcodeValidatorHandler _postalcodeHandler;
        private readonly ICountriesHandler _countriesHandler;
        private readonly IFormattersHandler _formattersHandler;

        public PostalCodeValidatorController(
            ILogger<PostalCodeValidatorController> logger,
            IPostalcodeValidatorHandler postalcodeHandler,
            ICountriesHandler countriesHandler,
            IFormattersHandler formatterHandler) 
        { 
            _logger = logger;
            _postalcodeHandler = postalcodeHandler;
            _countriesHandler = countriesHandler;
            _formattersHandler = formatterHandler;
        }

        [HttpPost(Name = "PostPostalcodeValidation")]
        [ProducesResponseType(typeof(PostalcodeValidationResponse), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> PostAsync([FromBody] PostalcodeValidaionRequest request, CancellationToken cancellationToken = default) 
        {
            try
            {
                var result = await _postalcodeHandler.HandleAsync(request, cancellationToken);

                return Ok(result);
            }
            catch (RequestException e) 
            { 
                return BadRequest(e.ErrorMessages);
            }
        }

        [HttpGet]
        [Route("countries")]
        [ProducesResponseType(typeof(IEnumerable<CountryResponse>), 200)]
        public async Task<IActionResult> GetCountriesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _countriesHandler.HandleAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("formatters")]
        [ProducesResponseType(typeof(IEnumerable<FormatterResponse>), 200)]
        public async Task<IActionResult> GetFormattersAsync(CancellationToken cancellationToken = default)
        {
            var result = await _formattersHandler.HandleAsync(cancellationToken);

            return Ok(result);
        }
    }
}