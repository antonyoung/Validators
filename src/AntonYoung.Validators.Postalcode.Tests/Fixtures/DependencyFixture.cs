using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AntonYoung.Validators.Postalcode.Tests.Fixtures
{
    public class DependencyFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
     
        public DependencyFixture()
        {
            ServiceCollection services = new();

            services.AddTransient<IPostalcodeModel, PostalcodeModel>();
            services.AddTransient<IPostalcodeValidator, PostalcodeValidator>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}