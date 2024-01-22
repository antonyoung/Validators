using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Client.Services
{
    internal class ValidatorService //: IHostedService
    {
        private readonly ILogger<ValidatorService> _logger;
        //private readonly CommandLineArgs _commandLineArgs;

        public ValidatorService (ILogger<ValidatorService> logger) //, CommandLineArgs arguments)
        {
            _logger = logger;
            ///_commandLineArgs = arguments;
        }

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("1. StartAsync has been called.");

        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("4. StopAsync has been called.");

        //    return Task.CompletedTask;
        //}

        public Task ValidateAsync(IEnumerable<string> arguments)
        {
            _logger.LogInformation("5. ValidateAsync {arguments}.", arguments);

            return Task.CompletedTask;
        }

        //private void OnStarted()
        //{
        //    _logger.LogInformation("2. OnStarted has been called.");
        //}

        //private void OnStopping()
        //{
        //    _logger.LogInformation("3. OnStopping has been called.");
        //}

        //private void OnStopped()
        //{
        //    _logger.LogInformation("5. OnStopped has been called.");
        //}
    }
}