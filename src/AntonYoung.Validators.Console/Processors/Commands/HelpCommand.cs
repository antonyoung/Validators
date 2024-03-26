using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Writters;

namespace AntonYoung.Validators.Console.Processors.Commands
{
    internal class HelpCommand
        : ICommandProcessor
    {
        private readonly IConsoleWritter _writer;

        public HelpCommand(IConsoleWritter writer)
            => _writer = writer;

        public async Task<Arguments> ProcessAsync(Arguments argument)
        {
            switch (argument)
            {
                case Arguments.Application:
                    await _writer.WriteHelpValidateAsync();
                    break;

                case Arguments.Country:
                    await _writer.WriteHelpCountriesAsync();
                    break;

                case Arguments.Formatter:
                    await _writer.WriteHelpFormattersAsync();
                    break;

                case Arguments.Validators:
                    break;

                case Arguments.Value:
                    //await _writer.WriteHelpApplicationAsync(_model.Application);
                    break;

                default:
                    break;
            }

            return Arguments.Unknown;
        }
    }
}
