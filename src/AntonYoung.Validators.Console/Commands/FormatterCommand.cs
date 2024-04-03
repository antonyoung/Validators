using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Extensions;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;

namespace AntonYoung.Validators.Console.Commands
{
    public class FormatterCommand(IConsoleWriter writer)
        : ICommand
    {
        public async Task<Arguments> ProcessAsync(string value, ValidatorModel model)
        {
            if (value.IsHelp())
            {
                await writer
                    .WriteHelpFormattersAsync();

                return Arguments.Help;
            }

            if (!Enum.TryParse<Formatters>(value, ignoreCase: true, out var formatter))
                throw new ArgumentException($"Unknown '{value}' as formatter option.");

            model.Formatter = formatter;

            return Arguments.Argument;
        }
    }
}
