using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;

namespace AntonYoung.Validators.Console.Commands
{
    public class ArgumentCommand
        : ICommand
    {
        public async Task<Arguments> ProcessAsync(string value, ValidatorModel model)
        {
            value = value.ToLowerInvariant();

            if (value == "--country" || value == "-c")
                return Arguments.Country;

            if (value == "--formatter" || value == "-f")
                return Arguments.Formatter;

            if (value == "--replace" || value == "-r")
                return Arguments.Replace;

            return await Task
                .FromResult(Arguments.Unknown);
        }
    }
}