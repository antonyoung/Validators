using Microsoft.Extensions.DependencyInjection;
using AntonYoung.Validators.Domain.Mappers;
using AntonYoung.Validators.Domain.Handlers;

namespace AntonYoung.Validators.Domain.Tests.Fixtures
{
    public class CountriesHandlerFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public CountriesHandlerFixture()
        {
            ServiceCollection services = new();

            services.AddTransient<ICountryMapper, CountryMapper>();
            services.AddTransient<ICountriesHandler, CountriesHandler>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}