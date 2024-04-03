using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Extensions;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;

namespace AntonYoung.Validators.Console.Commands
{
    public class ValidatorCommand(IConsoleWriter writer)
        : ICommand
    {
        public async Task<Arguments> ProcessAsync(string value, ValidatorModel model)
        {
            if (string.Equals(value, "--version", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "-v", StringComparison.OrdinalIgnoreCase))
            {
                await writer
                    .WriteVersionAsync();
                
                return Arguments.Version;
            }

            if (value.IsHelp())
            {
                await writer
                    .WriteHelpValidateAsync();
                
                return Arguments.Help;
            }

            return await new ApplicationCommand()
                .ProcessAsync(value, model);
        }
    }
}