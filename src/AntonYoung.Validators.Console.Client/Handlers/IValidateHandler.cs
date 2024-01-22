using AntonYoung.Validators.Console.Client.Enums;
using AntonYoung.Validators.Console.Client.Models;

namespace AntonYoung.Validators.Console.Client.Handlers
{
    internal interface IValidateHandler
    {
        bool IsValidateHandler(Applications application);
        Task HandleAsync(ValidatorModel model);
    }
}