using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Handlers;
using static System.Net.Mime.MediaTypeNames;

namespace AntonYoung.Validators.Console.Writters
{
    internal interface IConsoleWritter
    {
        Task WriteAsync(IbanValidationResponse response);
        Task WriteAsync(PostalcodeValidationResponse response);
        Task WriteHelpApplicationAsync(Applications application);
        Task WriteHelpCountriesAsync();
        Task WriteHelpFormattersAsync();
        Task WriteHelpValidateAsync();
        Task WriteVersionAsync();
    }

    internal class ConsoleWriter
        : IConsoleWritter
    {
        private readonly ICountriesHandler _countriesHandler;
        private readonly IFormattersHandler _formattersHandler;

        public ConsoleWriter(
            ICountriesHandler countriesHandler, 
            IFormattersHandler formattersHandler)
        {
            _countriesHandler = countriesHandler;
            _formattersHandler = formattersHandler;
        }

        public Task WriteAsync(IbanValidationResponse response) 
        {
            const string format = "{0,-20} = {1}";

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine(format, "Result", response.Result);
            System.Console.WriteLine(format, "IsValid", response.IsValid);
            System.Console.WriteLine(format, "Country", response.Country);
            System.Console.WriteLine(format, "AccountNumber", response.AccountNumber);
            System.Console.WriteLine(format, "CheckDigits", response.CheckDigits);
            System.Console.WriteLine(format, "NationalBankCode", response.NationalBankCode);
            System.Console.WriteLine(format, "NationalBranchCode", response.NationalBranchCode);
            System.Console.WriteLine(format, "NationalCheckDigit", response.NationalCheckDigit);
            System.Console.WriteLine(format, "ErrorMessage", response.ErrorMessage);
            System.Console.WriteLine(string.Empty);

            return Task.CompletedTask; 
        }

        public Task WriteAsync(PostalcodeValidationResponse response) 
        {
            const string format = "{0,-15} = {1}";

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine(format, "Result", response.Result);
            System.Console.WriteLine(format, "IsValid", response.IsValid);
            System.Console.WriteLine(format, "Country", response.Country);
            System.Console.WriteLine(format, "ErrorMessage", response.ErrorMessage);
            System.Console.WriteLine(string.Empty);

            return Task.CompletedTask;
        }

        public Task WriteHelpApplicationAsync(Applications application) 
        {
            // => validate iban| post --help

            // Description:
            // Validate Iban Command
            //
            // Usage:
            // validate Iban [< applicationArgument >] [options]
            //
            // Arguments:
            // <applicationArgument>  Argument passed to the application that is being validated. []
            // 
            // Options:
            //   -f, --format

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Description:");
            System.Console.WriteLine($"Validate {application} Command");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Usage:");
            if (application == Applications.Iban)
                System.Console.WriteLine($"validate {application} [< applicationArgument >] [options]");

            if(application == Applications.Post)
                System.Console.WriteLine($"validate {application} [< applicationArgument >] [command] [options]");

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Arguments:");
            System.Console.WriteLine("<applicationArgument>  Argument passed to the application that is being validated. []");
            System.Console.WriteLine(string.Empty);
            if (application == Applications.Post)
            {
                System.Console.WriteLine("Command:");
                System.Console.WriteLine("  -c, --country        Validate postalcode for given country as argument");
                System.Console.WriteLine(string.Empty);
            }
            System.Console.WriteLine("Options:");
            System.Console.WriteLine("  -f, --format         Used as formatter with given argument");
            System.Console.WriteLine("  -r, --replace        Used as replace value with given argument for the given formatter");
            System.Console.WriteLine(string.Empty);

            return Task.CompletedTask;
        }

        public async Task WriteHelpCountriesAsync() 
        {
            // => validate post --country --help

            var countries = await _countriesHandler.HandleAsync(cancellationToken: default);

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Description:");
            System.Console.WriteLine($"Validate Post [< applicationArgument >] Country");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine($"validate Post [< applicationArgument >] [Country] [options]");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Arguments:");
            System.Console.WriteLine("<applicationArgument>  Argument passed to the application that is being validated. []");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Optons:");
            System.Console.WriteLine("  {0,-15}{1,-10}{2}", "EnumType", "ISO3", "ISO2");
            System.Console.WriteLine("  -----------------------------");

            foreach (var country in countries)
            {
                System.Console.WriteLine("  {0,-15}{1,-10}{2}", country.EnglishName, country.ThreeLetterISO, country.TwoLetterISO);
            }

            System.Console.WriteLine(string.Empty);
        }

        public async Task WriteHelpFormattersAsync() 
        {
            // => validate iban| post --formatter --help

            var formatters = await _formattersHandler.HandleAsync(cancellationToken: default);

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Description:");
            System.Console.WriteLine($"Validate Iban| Post [< applicationArgument >] Formatter");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine($"validate Iban| Post [< applicationArgument >] [Formatter] [options]");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Arguments:");
            System.Console.WriteLine("<applicationArgument>  Argument passed to the application that is being validated. []");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Optons:");

            foreach (var formatter in formatters) 
            {
                System.Console.WriteLine("  {0,-25}* {1}", formatter.Formatter, formatter.Description);
            }
            
            System.Console.WriteLine(string.Empty);
        }

        public Task WriteHelpValidateAsync()
        {
            // => validate --help

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Usage: validate [sdk-options] [command] [command-options] [arguments]");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Execute Validate SDK command.");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("sdk-options:");
            System.Console.WriteLine("-h|--help       Show command line help.");
            System.Console.WriteLine("--version       Display .NET SDK version in use.");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("SDK commands:");
            System.Console.WriteLine("   iban           Validate an international bank account number (IBAN).");
            System.Console.WriteLine("   post           Validate a Postalcode");
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Run 'validate [command] --help' for more information on a command.");
            System.Console.WriteLine(string.Empty);

            return Task.CompletedTask;
        }

        public Task WriteVersionAsync()
        {
            /// TODO:
            
            var result = GetType()?.Assembly?.GetName()?.Version?.ToString();

            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine(result);

            return Task.CompletedTask;
        }
    }
}