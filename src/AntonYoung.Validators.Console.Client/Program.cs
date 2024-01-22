using AntonYoung.Validators.Console.Client.Services;
using AntonYoung.Validators.Iban;
using AntonYoung.Validators.Iban.Models;
using AntonYoung.Validators.Iban.Infrastructure;
using AntonYoung.Validators.Postalcode;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AntonYoung.Validators.Console.Client
{ 
    public class CommandLineArgs
    {
        public string Args { get; set; }
    }

    internal class Program
    {
        public static IConfiguration Configuration { get; set; }

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
                    //services.AddCommandLineOptions(param);
                    //services.AddScoped<ValidatorService>();

                    //services.AddSingleton<IIbanModel, IbanModel>();
                    //services.AddSingleton<IPostalcodeModel, PostalcodeModel>();
                    services.AddTransient<IIbanValidator, IbanValidator>();
                    services.AddTransient<IPostalcodeValidator, PostalcodeValidator>();
                })
                .UseSerilog()
                .Build();

            //var param = System.Console.ReadLine();

            ValidatorService? service = ActivatorUtilities.CreateInstance<ValidatorService>(host.Services);

            //var param = "validate iban NL47INGB00789769";

            var result = service.ValidateAsync(args.AsEnumerable<string>());

            var commandLIne = System.Console.ReadLine();

            
           await Main(commandLIne?.Split(" "));


            return await Task.FromResult(Environment.ExitCode);
            //return await Task.FromResult(Environment.ExitCode);

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