using AntonYoung.Validators.Domain.Handlers;
using AntonYoung.Validators.Domain.Mappers;
using AntonYoung.Validators.Domain.Validators;
using AntonYoung.Validators.Iban;
using AntonYoung.Validators.Iban.Infrastructure;
using AntonYoung.Validators.Iban.Models;
using AntonYoung.Validators.Postalcode;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace AntonYoung.Validators.Domain.Infrastructure.DependencyInjection
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddValidatorsDomain(this IServiceCollection services) 
        {
            /// TODO: Move into AntonYoung.Validators.Postalcode.Infrastructure.DependecyInjection
            services.TryAddTransient<IPostalcodeModel, PostalcodeModel>();
            services.TryAddTransient<IPostalcodeValidator, PostalcodeValidator>();

            services.TryAddTransient<ICountryMapper, CountryMapper>();
            services.TryAddTransient<ICountriesHandler, CountriesHandler>();
            services.TryAddTransient<IFormattersHandler, FormattersHandler>();

            services.TryAddTransient<IPostalcodeRequestValidator, PostalcodeRequestValidator>();
            services.TryAddTransient<IPostalcodeValidatorHandler, PostalcodeValidatorHandler>();

            /// TODO: Move into AntonYoung.Validators.Iban.Infrastructure.DependecyInjection
            services.TryAddTransient<IIbanModel, IbanModel>();
            services.TryAddTransient<IIbanValidator, IbanValidator>();
            
            services.TryAddTransient<IIbanValidatorHandler, IbanValidatorHandler>();
            services.TryAddTransient<IIbanRequestValidator, IbanRequestValidator>();

            return services;
        }
    }
}
