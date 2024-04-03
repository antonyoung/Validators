using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Extensions;
using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Console.Writters;

namespace AntonYoung.Validators.Console.Commands
{
    public class CountryCommand(IConsoleWriter writer)
        : ICommand
    {
        public async Task<Arguments> ProcessAsync(string value, ValidatorModel model)
        {
            if (value.IsHelp())
            {
                await writer
                    .WriteHelpCountriesAsync();

                return Arguments.Help;
            }

            model.Country = value;

            return Arguments.Argument;
        }
    }
}