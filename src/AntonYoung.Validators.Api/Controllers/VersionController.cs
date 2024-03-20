using Microsoft.AspNetCore.Mvc;

namespace AntonYoung.Validators.Api.Controllers
{
    [Route("version")]
    [ApiController]
    public class VersionController 
        : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetVersionAsync(CancellationToken cancellationToken = default)
        {
            var version = GetType()?.Assembly?
                .GetName()?.Version?
                .ToString();

            return await Task.FromResult(Ok(version));

        }
    }
}