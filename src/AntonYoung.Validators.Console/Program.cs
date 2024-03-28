using AntonYoung.Validators.Console.Infrastructure.DependencyInjection;
using AntonYoung.Validators.Console.Processors;
using AntonYoung.Validators.Console.Services;
using AntonYoung.Validators.Domain.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AntonYoung.Validators.Console
{
    internal static class Program
    {
        //public static IConfiguration Configuration { get; set; }

        public static void ConfigurationSetup(IConfigurationBuilder builder) => builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        //public static IHost StartUp()
        //{
        //    var builder = new ConfigurationBuilder();
        //    ConfigurationSetup(builder);

        //    Log.Logger = new LoggerConfiguration()
        //        .ReadFrom.Configuration(builder.Build())
        //        .WriteTo.Console()
        //        .CreateLogger();

        //    var host = Host.CreateDefaultBuilder()
        //        .ConfigureServices((context, Services) =>
        //        {

        //        })
        //        .UseSerilog()
        //        .Build();

        //    return host;
        //}

        public static async Task<int> Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            //Host.CreateDefaultBuilder(args)
            //    .ConfigureServices((hostContext, services) =>
            //    {
            //        //services.AddSingleton(new CommandLineArgs { Args = param });
            //        //services.AddCommandLineOptions(param);
            //         //services.AddOptions<FileUtilOptions>()
            //         //   .Configure(opt =>
            //         //       Parser.Default.ParseArguments(() => opt, Environment.GetCommandLineArgs()
            //         //   )
            //        services.AddHostedService<ValidatorService>();
            //    });

            var builder = new ConfigurationBuilder();

            ConfigurationSetup(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .WriteTo.Console()
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddValidatorsDomain();
                    services.AddConsole();
                })
                .UseSerilog()
                .Build();

            CommandLineProcessor? processor = ActivatorUtilities
                .CreateInstance<CommandLineProcessor>(host.Services);

            try
            {
                var model = await processor
                    .ProcessAsync(args.AsEnumerable<string>());

                if (!model.IsHelp)
                {
                    ValidatorService? service = ActivatorUtilities
                        .CreateInstance<ValidatorService>(host.Services);

                    await service
                        .ValidateAsync(model);
                }
            }
            catch (Exception e)
            {
                System.Console.Error.WriteLine(e.Message);
                System.Console.WriteLine("");
            }

            var commandLine = System.Console
                .ReadLine()?
                .Split(" ") ?? [];
            
            await Main(commandLine);

            return await Task
                .FromResult(Environment.ExitCode);
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureServices((hostContext, services) =>
        //        {
        //            services.AddSingleton(new CommandLineArgs { Args = param });
        //            services.AddHostedService<ValidatorService>();
        //        });
    }
}