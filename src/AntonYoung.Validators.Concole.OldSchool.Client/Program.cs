using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Iban;
using AntonYoung.Validators.Iban.Infrastructure;
using AntonYoung.Validators.Iban.Models;
using AntonYoung.Validators.Postalcode;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AntonYoung.Validators.Concole.OldSchool.Client
{
    internal static class Program
    {
        internal enum Applications
        {
            Iban,
            PostalCode
        }

        internal enum Arguments
        {
            Application,
            Argument,
            Country,
            Formatter,
            Replace,
            Validators,
            Value,
            Unknown
        }

        public class ValidatorModel
        {
            internal Applications Application { get; set; }
            internal Countries Country { get; set; } = Countries.Netherlands;
            internal Formatters Formatter { get; set; } = Formatters.None;
            internal string Replace { get; set; } = string.Empty;
            internal string Value { get; set; } = string.Empty;
        }

        public static IConfiguration? Configuration { get; set; }

        public static void ConfigurationSetup(IConfigurationBuilder builder) => builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // TODO:

            // 01. Configuration
            var builder = new ConfigurationBuilder();
            ConfigurationSetup(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .WriteTo.Console()
                .CreateLogger();

            // 02. Dependency injection
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                { 
                    services.AddSingleton<IIbanModel, IbanModel>();
                    services.AddSingleton<IPostalcodeModel, PostalcodeModel>();
                    services.AddTransient<IIbanValidator, IbanValidator>();
                    services.AddTransient<IPostalcodeValidator, PostalcodeValidator>();
                })
                .UseSerilog()
                .Build();

            // 03. Read Arguments
            var model = new ValidatorModel();
            var exitCode = ProcessArguments(args, model);

            // 04. Validate Arguments
            if (exitCode == -1)
            {
                Console.WriteLine("ErroCode");

                return;
            }
            
            // 5. Result Arguments
            if (model.Application == Applications.PostalCode)
            {
                var postalCodeValidator = host.Services.GetRequiredService<IPostalcodeValidator>();

                var result = postalCodeValidator.TryValidate(model.Value, model.Country, model.Formatter, model.Replace, out var postalCodeResult);
                Console.WriteLine(postalCodeResult);
            }
            
            if (model.Application == Applications.Iban)
            {
                var ibanValidator = host.Services.GetRequiredService<IIbanValidator>();

                var result = ibanValidator.TryValidate(model.Value, model.Formatter, model.Replace, out var ibanResult);
                Console.WriteLine(ibanResult);
            }
    
            // 06. Quit or Revalidate new Arguments
            var commandLine = Console.ReadLine();

            Main(commandLine?.Split(" "));
        }

        private static int ProcessArguments(string[] arguments, ValidatorModel model)
        {
            if (!arguments.Any())
            {
                Console.Error.WriteLine("AntonYoung.Validators.Concole.OldSchool.Client: No arguments specified.");
                Console.Error.WriteLine("Try `validate --help' for available options.");

                return -1;
            }

            var scan = Arguments.Argument;
            int exitCode = 0;

            foreach (var argument in arguments)
            {
                switch (scan)
                {
                    case Arguments.Argument:
                        scan  = ProcessCommand(argument, model);
                        break;

                    case Arguments.Application:
                        model.Application
                            = Enum.TryParse<Applications>(argument, ignoreCase: true, out var application)
                            ? application
                            : throw new ArgumentException(nameof(application));

                        scan = ProcessCommand(argument, model);

                        break;

                    case Arguments.Country:
                        model.Country
                            = Enum.TryParse<Countries>(argument, ignoreCase: true, out var country)
                            ? country
                            : throw new ArgumentException(nameof(country));

                        scan = Arguments.Argument;

                        break;

                    case Arguments.Formatter:
                        model.Formatter
                            = Enum.TryParse<Formatters>(argument, ignoreCase: true, out var formatter)
                            ? formatter
                            : throw new ArgumentException(nameof(formatter));

                        scan = Arguments.Argument;

                        break;

                    case Arguments.Replace:
                        model.Replace = argument;
                        scan = Arguments.Argument;

                        break;

                    case Arguments.Validators:
                        scan = ProcessCommand(argument, model);

                        break;

                    case Arguments.Value:
                        model.Value = argument;
                        scan = Arguments.Argument;

                        break;

                    case Arguments.Unknown:
                        exitCode = -1;

                        break;

                    default:
                        exitCode = -1;
                        
                        break;
                }
            }

            return exitCode;
        }

        private static Arguments ProcessCommand(string value, ValidatorModel model)
        {
            var result = Arguments.Argument;

            switch (value.Trim().ToLower())
            {
                //=> validators
                case "validate":
                    result = Arguments.Application;
                    break;
                case "iban":
                    model.Application = Applications.Iban;
                    result = Arguments.Value;
                    break;
                case "postalcode":
                    model.Application = Applications.PostalCode;
                    result = Arguments.Value;
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
