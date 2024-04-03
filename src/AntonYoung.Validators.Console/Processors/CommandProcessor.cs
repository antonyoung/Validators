using AntonYoung.Validators.Console.Commands;
using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Writters;

namespace AntonYoung.Validators.Console.Processors
{
    public interface ICommandProcessor
    {
        Task<ICommand> ProcessAsync(Arguments argument);
    }

    public class CommandProcessor(IConsoleWriter writer)
        : ICommandProcessor
    {
        public async Task<ICommand> ProcessAsync(Arguments argument)
        {
            ICommand command = new ArgumentCommand();

            switch (argument) 
            {
                case Arguments.Application:
                    command = new ApplicationCommand();
                    break;

                case Arguments.Argument: 
                    command = new ArgumentCommand();
                    break;

                case Arguments.Country:
                    command = new CountryCommand(writer);
                    break;

                case Arguments.Formatter:
                    command = new FormatterCommand(writer);
                    break;

                case Arguments.Replace:
                    command = new ReplaceCommand();
                    break;

                case Arguments.Value:
                    command = new ValueCommand(writer);
                    break;

                case Arguments.Validators:
                    command = new ValidatorCommand(writer);
                    break;
            }

            return await Task
                .FromResult(command);
        }
    }
}
