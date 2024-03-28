using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;

namespace AntonYoung.Validators.Console.Commands
{
    internal class ApplicationCommand
        : ICommand
    {
        public Task<Arguments> ProcessAsync(string value, ValidatorModel model)
        {
            if (!Enum.TryParse<Applications>(value, ignoreCase: true, out var application))
                throw new ArgumentException($"Unknown '{value}' as SDK command.");

            model.Application = application;

            return Task
                .FromResult(Arguments.Value);
        }
    }
}