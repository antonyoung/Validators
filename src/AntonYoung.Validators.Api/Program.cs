using AntonYoung.Validators.Domain.Infrastructure.DependencyInjection;
using AntonYoung.Validators.Api.HealthChecks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AntonYoung.Validators.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add validation services to the container.
            builder.Services.AddValidatorsDomain();

            // Add health checks services to the container.
            builder.Services.AddHealthChecks()
                .AddCheck<PingHealthCheck>(nameof(PingHealthCheck), tags: new List<string> { "Ping", "Live" });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // health-check endpoint path
            app.UseHealthChecks("/health");

            app.MapControllers();

            app.Run();
        }
    }
}