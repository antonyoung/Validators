using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Models;

namespace AntonYoung.Validators.Console.Commands
{
    public interface ICommand
    {
        Task<Arguments> ProcessAsync(string value, ValidatorModel model);
    }
}