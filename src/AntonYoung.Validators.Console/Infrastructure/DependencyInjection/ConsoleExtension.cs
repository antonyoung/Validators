using AntonYoung.Validators.Console.Mappers;
using AntonYoung.Validators.Console.Processors;
using AntonYoung.Validators.Console.Services;
using AntonYoung.Validators.Console.Writters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AntonYoung.Validators.Console.Infrastructure.DependencyInjection
{
    public static class ConsoleExtension
    {
        public static IServiceCollection AddConsole(this IServiceCollection services)
        {       
            services.TryAddTransient<IIbanRequestMapper, IbanRequestMapper>();
            services.TryAddTransient<IPostalcodeRequestMapper, PostalcodeRequestMapper>();
            services.TryAddTransient<IConsoleWriter, ConsoleWriter>();
            services.TryAddTransient<ICommandProcessor, CommandProcessor>();
            services.TryAddTransient<ICommandlineProcessor, CommandLineProcessor>();
            services.TryAddTransient<IValidatorService, ValidatorService>();

            return services;
        }
    }
}