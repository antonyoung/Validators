using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Client.Enums;
using AntonYoung.Validators.Console.Client.Models;

namespace AntonYoung.Validators.Console.Client.Processors
{
    internal class ArgumentProcessor
    {
        private readonly ValidatorModel _model;

        public ArgumentProcessor()
            => _model = new ValidatorModel();

        public ValidatorModel Process(string[] argumennts)
        {
            var exitCode = ProcessArguments(argumennts);

            return _model;
        }

        private int ProcessArguments(string[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
            {
                //Console.Error.WriteLine("vwd.Factsheets.Generator: No arguments specified.");
                //Console.Error.WriteLine("Try `--help' for available options.");

                return -1;
            }

            var scan = Arguments.Application;

            foreach (var argument in arguments)
            {
                switch (scan)
                {
                    case Arguments.Argument:
                        break;

                    case Arguments.Application:
                        _model.Application
                            = Enum.TryParse<Applications>(argument, out var application)
                            ? application
                            : throw new ArgumentException(nameof(application));

                        break;

                    case Arguments.Country:
                        _model.Country 
                            = Enum.TryParse<Countries>(argument, out var country)
                            ? country
                            : throw new ArgumentException(nameof(country));
                        
                        break;

                    case Arguments.Formatter:
                        _model.Formatter
                            = Enum.TryParse<Formatters>(argument, out var formatter)
                            ? formatter
                            : throw new ArgumentException(nameof(formatter));
                        
                        break;

                    case Arguments.Replace:
                        _model.Replace = argument;
                        break;


                    case Arguments.Validators:
                        break;

                    case Arguments.Unknown:
                        break;

                    default:
                        break;
                }
            }

            return -1;
        }

        private Arguments ProcessCommand(string value)
        {
            var result = Arguments.Argument;

            switch (value.Trim().ToLower())
            {
                //=> validators
                case "validate":
                    result = Arguments.Validators;
                    break;
                case "iban":
                    _model.Application = Applications.Iban;
                    result = Arguments.Argument;
                    break;
                case "postalcode":
                    _model.Application = Applications.PostalCode;
                    result = Arguments.Argument;
                    break;
                case "-c":
                case "-country":
                case "--c":
                case "--country":
                case "/c":
                case "/country":
                    result = Arguments.Country;
                    break;
                case "-f":
                case "-formatter":
                case "--f":
                case "--formatter":
                case "/f":
                case "/formatter":
                    result = Arguments.Formatter;
                    break;
                case "-r":
                case "-replace":
                case "--r":
                case "--replace":
                case "/r":
                case "/replace":
                    result = Arguments.Replace;
                    break;
                default:
                    result = Arguments.Unknown;
                    break;
            }

            return result;
        }
    }
}