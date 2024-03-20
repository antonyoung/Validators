using AntonYoung.Validators.Domain.Abstractions.Exceptions;
using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace AntonYoung.Validators.Api.Controllers
{
    [ApiController]
    [Route("validator/iban")]
    public class IbanValidatorController : ControllerBase
    {
        private readonly ILogger<IbanValidatorController> _logger;
        private readonly IIbanValidatorHandler _ibanHandler;
        private readonly IFormattersHandler _formattersHandler;

        public IbanValidatorController(
            ILogger<IbanValidatorController> logger,
            IIbanValidatorHandler ibanHandler,
            IFormattersHandler formattersHandler) 
        { 
            _logger = logger;
            _ibanHandler = ibanHandler;
            _formattersHandler = formattersHandler;
        }

        [HttpPost(Name = "PostIbanValidation")]
        [ProducesResponseType(typeof(IbanValidationResponse), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> PostAsync([FromBody] IbanValidationRequest request, CancellationToken cancellationToken = default) 
        {
            try
            { 
                var result = await _ibanHandler.HandleAsync(request, cancellationToken);

                return Ok(result);
            }
            catch (RequestException e) 
            {
                return BadRequest(e.ErrorMessages);
            }
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