using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Handlers;
using AntonYoung.Validators.Domain.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AntonYoung.Validators.Domain.Tests.Handlers
{
    public class CountriesHandlerTests
        : IClassFixture<CountriesHandlerFixture>
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IEnumerable<CountryResponse> _countries = new List<CountryResponse>
        {
            new() 
            {
                EnglishName = "Amsterdam",
                ThreeLetterISO = string.Empty,
                TwoLetterISO = string.Empty
            },
            new()
            {
                EnglishName = "Austria",
                ThreeLetterISO = "AUT",
                TwoLetterISO = "AT",
            },
            new()
            {
                EnglishName = "Belgium",
                ThreeLetterISO = "BEL",
                TwoLetterISO = "BE"
            },
            new()
            {
                EnglishName = "Bulgaria",
                ThreeLetterISO = "BGR",
                TwoLetterISO = "BG"
            },
            new()
            {
                EnglishName = "Croatia",
                ThreeLetterISO = "HRV",
                TwoLetterISO = "HR"
            },
            new()
            {
                EnglishName = "Cyprus",
                ThreeLetterISO = "CYP",
                TwoLetterISO = "CY"
            },
            new()
            {
                EnglishName = "Czechia",
                ThreeLetterISO = "CZE",
                TwoLetterISO = "CZ"
            },
            new()
            {
                EnglishName = "Denmark",
                ThreeLetterISO = "DNK",
                TwoLetterISO = "DK"
            },
            new()
            {
                EnglishName = "Estonia",
                ThreeLetterISO = "EST",
                TwoLetterISO = "EE"
            },
            new()
            {
                EnglishName = "Finland",
                ThreeLetterISO = "FIN",
                TwoLetterISO = "FI"
            },
            new()
            {
                EnglishName = "France",
                ThreeLetterISO = "FRA",
                TwoLetterISO = "FR"
            },
            new()
            {
                EnglishName = "Germany",
                ThreeLetterISO = "DEU",
                TwoLetterISO = "DE"
            },
            new()
            {
                EnglishName = "Greece",
                ThreeLetterISO = "GRC",
                TwoLetterISO = "GR"
            },
            new()
            {
                EnglishName = "Hungary",
                ThreeLetterISO = "HUN",
                TwoLetterISO = "HU"
            },
            new()
            {
                EnglishName = "Ireland",
                ThreeLetterISO = "IRL",
                TwoLetterISO = "IE"
            },
            new()
            {
                EnglishName = "Latvia",
                ThreeLetterISO = "LVA",
                TwoLetterISO = "LV"
            },
            new()
            {
                EnglishName = "Lithuania",
                ThreeLetterISO = "LTU",
                TwoLetterISO = "LT"
            },
            new()
            {
                EnglishName = "Italy",
                ThreeLetterISO = "ITA",
                TwoLetterISO = "IT"
            },
            new()
            {
                EnglishName = "Luxembourg",
                ThreeLetterISO = "LUX",
                TwoLetterISO = "LU"
            },
            new()
            {
                EnglishName = "Malta",
                ThreeLetterISO = "MLT",
                TwoLetterISO = "MT"
            },
            new()
            {
                EnglishName = "Netherlands",
                ThreeLetterISO = "NLD",
                TwoLetterISO = "NL"
            },
            new()
            {
                EnglishName = "Poland",
                ThreeLetterISO = "POL",
                TwoLetterISO = "PL"
            },
            new()
            {
                EnglishName = "Portugal",
                ThreeLetterISO = "PRT",
                TwoLetterISO = "PT"
            },
            new()
            {
                EnglishName = "Romania",
                ThreeLetterISO = "ROU",
                TwoLetterISO = "RO"
            },
            new()
            {
                EnglishName = "Slovakia",
                ThreeLetterISO = "SVK",
                TwoLetterISO = "SK"
            },
            new()
            {
                EnglishName = "Slovenia",
                ThreeLetterISO = "SVN",
                TwoLetterISO = "SI"
            },
            new()
            {
                EnglishName = "Spain",
                ThreeLetterISO = "ESP",
                TwoLetterISO = "ES"
            },
            new()
            {
                EnglishName = "Sweden",
                ThreeLetterISO = "SWE",
                TwoLetterISO = "SE"
            },
            new()
            {
                EnglishName = "UnitedKingdom",
                ThreeLetterISO = "GBR",
                TwoLetterISO = "GB"
            }
        };

        public CountriesHandlerTests(CountriesHandlerFixture fixture)
            => _serviceProvider = fixture.ServiceProvider;

        [Fact]
        public async Task All()
        {
            var handler = _serviceProvider
                .GetService<ICountriesHandler>()!;

            var result = await handler
                .HandleAsync(CancellationToken.None);

            result
                .Should()
                .HaveCount(29);

            result
                .Should()
                .BeEquivalentTo(_countries);
        }
    }
}