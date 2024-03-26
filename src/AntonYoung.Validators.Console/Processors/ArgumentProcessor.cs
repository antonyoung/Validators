using AntonYoung.Validators.Console.Enums;

namespace AntonYoung.Validators.Console.Processors
{
    internal interface IArgumentProcessor
    {
        Task<Arguments> GetAsync(string argument);
    }

    internal class ArgumentProcessor
        : IArgumentProcessor
    {
        private readonly IDictionary<string, Arguments> _arguments = new Dictionary<string, Arguments>
        {
            { "--country", Arguments.Country },
            { "-c", Arguments.Country },
            { "iban", Arguments.Application },
            { "post", Arguments.Application },
            { "--formatter", Arguments.Formatter },
            { "-f", Arguments.Formatter },
            { "--replace", Arguments.Replace },
            { "-r", Arguments.Replace },
            { "--version", Arguments.Version },
            { "-v", Arguments.Version },
            { "--help", Arguments.Help },
            { "-h", Arguments.Help },
            { "validate", Arguments.Validators }
        };

        public async Task<Arguments> GetAsync(string argument)
        {
           if (!_arguments.TryGetValue(argument, out var result))
                return Arguments.Unknown;

            return await Task.FromResult(result);
        }
    }
}