using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;
using Microsoft.Extensions.Logging;

namespace AntonYoung.Validators.Console.Processors
{

    internal interface ICommandlineProcessor2
    {
        //Task<ValidatorModel> ProcessAsync(IEnumerable<string> argumennts);
        //Arguments ProcessCommand(string value);
    }

    internal class ComandLineProcessor2
        : ICommandlineProcessor2
    {
        private readonly ILogger<ComandLineProcessor> _logger;
        private readonly IArgumentProcessor _argumentProcessor;
        private readonly IConsoleWritter _writer;
        private readonly ValidatorModel _model;

        public ComandLineProcessor2(
            ILogger<ComandLineProcessor> logger,
            IArgumentProcessor argumentProcessor,
            IConsoleWritter writter)
        {
            _logger = logger;
            _argumentProcessor = argumentProcessor;
            _writer = writter;
            _model = new ValidatorModel();
        }

        public async Task<ValidatorModel> ProcessAsync(IEnumerable<string> arguments)
        {
            if (!arguments.Any())
            {
                System.Console.Error.WriteLine("AntonYoung.Validators.Concole: No arguments specified.");
                System.Console.Error.WriteLine("Try `validate --help' for available options.");
                System.Console.WriteLine(string.Empty);

                //return -1;
            }

            if (arguments.First().ToLower() != "validate")
            {
                System.Console.Error.WriteLine("ERROR");
            }

            ProcessArguments(arguments);

            return await Task.FromResult(_model);
        }

        private int ProcessArguments(IEnumerable<string> arguments)
        {
            // 1. first has to be "validate"
            // 2. second has to be "help" or as application iban | post
            // 3. 
            //if (!arguments.Any())
            //{
            //    System.Console.Error.WriteLine("AntonYoung.Validators.Concole: No arguments specified.");
            //    System.Console.Error.WriteLine("Try `validate --help' for available options.");
            //    System.Console.WriteLine(string.Empty);

            //    return -1;
            //}

            //var scan = Arguments.Validators;

            foreach (var argument in arguments)
            {
                if (_model.IsHelp) break;

                var test = _argumentProcessor.GetAsync(argument);
            //    switch (scan)
            //    {
            //        case Arguments.Argument:
            //            scan = ProcessCommand(argument);
            //            break;

            //        case Arguments.Application:
            //            if (ProcessHelp(scan, argument))
            //            {
            //                _model.IsHelp = true;
            //                break;
            //            }

            //            _model.Application
            //                = Enum.TryParse<Applications>(argument, ignoreCase: true, out var application)
            //                ? application
            //                : throw new ArgumentException(nameof(application));

            //            scan = Arguments.Value;
            //            break;

            //        case Arguments.Country:
            //            if (ProcessHelp(scan, argument))
            //            {
            //                _model.IsHelp = true;
            //                break;
            //            }

            //            _model.Country = argument;
            //            scan = Arguments.Argument;
            //            break;

            //        case Arguments.Formatter:
            //            if (ProcessHelp(scan, argument))
            //            {
            //                _model.IsHelp = true;
            //                break;
            //            }

            //            _model.Formatter
            //                = Enum.TryParse<Formatters>(argument, ignoreCase: true, out var formatter)
            //                ? formatter
            //                : throw new ArgumentException(nameof(formatter));
            //            scan = Arguments.Argument;
            //            break;

            //        case Arguments.Replace:
            //            _model.Replace = argument;
            //            scan = Arguments.Argument;
            //            break;

            //        case Arguments.Validators:
            //            if (ProcessHelp(scan, argument))
            //            {
            //                _model.IsHelp = true;
            //                break;
            //            }

            //            scan = ProcessCommand(argument);
            //            break;

            //        case Arguments.Value:
            //            if (ProcessHelp(scan, argument))
            //            {
            //                _model.IsHelp = true;
            //                break;
            //            }

            //            _model.Value = argument;
            //            scan = Arguments.Argument;
            //            break;

            //        case Arguments.Version:
            //            _model.IsHelp = true;
            //            _writer.WriteVersionAsync();
            //            break;

            //        case Arguments.Unknown:
            //            break;

            //        default:
            //            break;
            //    }
            }

            return -1;
        }

        public Arguments ProcessCommand(string value)
        {
            Arguments result;

            switch (value.Trim().ToLower())
            {
                case "validate":
                    result = Arguments.Application;
                    break;

                case "iban":
                    _model.Application = Applications.Iban;
                    result = Arguments.Value;
                    break;

                case "post":
                    _model.Application = Applications.Post;
                    result = Arguments.Value;
                    break;

                case "-c":
                case "--country":
                    result = Arguments.Country;
                    break;

                case "-f":
                case "--formatter":
                    result = Arguments.Formatter;
                    break;

                case "-r":
                case "--replace":
                    result = Arguments.Replace;
                    break;

                case "-v":
                case "--version":
                    result = Arguments.Version;
                    break;

                default:
                    result = Arguments.Unknown;
                    break;
            }

            return result;
        }

        private bool ProcessHelp(Arguments argument, string value)
        {
            bool result = false;

            if (value == "--help" || value == "-h")
            {
                switch (argument)
                {
                    case Arguments.Application:
                        _writer.WriteHelpValidateAsync();
                        break;

                    case Arguments.Country:
                        _writer.WriteHelpCountriesAsync();
                        break;

                    case Arguments.Formatter:
                        _writer.WriteHelpFormattersAsync();
                        break;

                    case Arguments.Validators:

                        break;

                    case Arguments.Value:
                        _writer.WriteHelpApplicationAsync(_model.Application);
                        break;

                    default:

                        break;
                }

                result = true;
            }

            if (value == "-v" || value == "--version")
            {
                _writer.WriteVersionAsync();
                result = true;
            }

            return result;
        }
    }
}
