using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;

namespace AntonYoung.Validators.Console.Commands
{
    internal class ReplaceCommand
        : ICommand
    {
        public async Task<Arguments> ProcessAsync(string value, ValidatorModel model)
        {
            model.Replace = value;

            return await Task
                .FromResult(Arguments.Argument);
        }
    }
}