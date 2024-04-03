using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Extensions;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;

namespace AntonYoung.Validators.Console.Commands
{
    public class ValueCommand(IConsoleWriter writer)
        : ICommand
    {
        public async Task<Arguments> ProcessAsync(string value, ValidatorModel model)
        {
            if (value.IsHelp())
            {
                await writer
                    .WriteHelpApplicationAsync(model.Application);

                return Arguments.Help;
            }

            if (string.IsNullOrEmpty(value))
                return Arguments.Unknown;

            model.Value = value;

            return Arguments.Argument;
        }
    }
}
