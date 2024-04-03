using AntonYoung.Validators.Console.Commands;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Processors
{
    public interface ICommandlineProcessor
    {
        Task<ValidatorModel> ProcessAsync(IEnumerable<string> argumennts);
    }

    public class CommandLineProcessor(
        ILogger<CommandLineProcessor> logger,
        ICommandProcessor processor) : ICommandlineProcessor
    {
        private readonly ILogger<CommandLineProcessor> _logger = logger;
        private readonly ICommandProcessor _processor = processor;
        private readonly ValidatorModel _model = new();

        public async Task<ValidatorModel> ProcessAsync(IEnumerable<string> arguments)
        {
            if (!arguments.Any()
                || !string.Equals(arguments.First(), "validate", StringComparison.OrdinalIgnoreCase))
            {
                System.Console.Error.WriteLine("AntonYoung.Validators.Console: No arguments specified.");
                System.Console.Error.WriteLine("Try `validate --help' for available options.");
                System.Console.WriteLine(string.Empty);

                _model.IsHelp = true;

                return _model;
            }

            Arguments scan = Arguments.Validators;

            foreach (var argument in arguments.Skip(1))
            {
                if (scan == Arguments.Help 
                    || scan == Arguments.Version
                    || scan == Arguments.Unknown)
                    break;

                ICommand command = await _processor
                    .ProcessAsync(scan);

                scan = await command
                    .ProcessAsync(argument.ToLower(), _model);

                if (scan == Arguments.Unknown)
                    System.Console.Error.WriteLine($"Unknown '{argument}' as commandline argument.");
            }

            _model.IsHelp = scan == Arguments.Help 
                || scan == Arguments.Version
                || scan == Arguments.Unknown;

            return await Task
                .FromResult(_model);
        }
    }
}