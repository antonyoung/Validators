using AntonYoung.Validators.Iban.Infrastructure;
using AntonYoung.Validators.Iban.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AntonYoung.Validators.Iban.Tests.Fixtures
{
    public class DependencyFixture
    { 
        public ServiceProvider ServiceProvider { get; private set; }

        public DependencyFixture()
        {
            ServiceCollection services = new();

            services.AddTransient<IIbanModel, IbanModel>();
            services.AddTransient<IIbanValidator, IbanValidator>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}