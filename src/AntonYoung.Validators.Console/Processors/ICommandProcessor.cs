using AntonYoung.Validators.Console.Enums;

namespace AntonYoung.Validators.Console.Processors
{
    internal interface ICommandProcessor
    {
        Task<Arguments> ProcessAsync(Arguments argument);
    }
}
