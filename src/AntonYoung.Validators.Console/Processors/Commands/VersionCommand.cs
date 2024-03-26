using AntonYoung.Validators.Console.Enums;
using AntonYoung.Validators.Console.Writters;

namespace AntonYoung.Validators.Console.Processors.Commands
{
    internal class VersionCommand
        : ICommandProcessor
    {
        private readonly IConsoleWritter _writer;

        public VersionCommand(IConsoleWritter writer)
            => _writer = writer;

        public async Task<Arguments> ProcessAsync(Arguments argument)
        {
            await _writer.WriteVersionAsync();

            return Arguments.Unknown;
        }
    }
}