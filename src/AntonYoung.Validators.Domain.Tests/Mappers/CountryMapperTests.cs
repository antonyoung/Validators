using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Mappers;
using FluentAssertions;
using Xunit;

namespace AntonYoung.Validators.Domain.Tests.Mappers
{
    public class CountryMapperTests
    {
        private readonly ICountryMapper _mapper;

        public CountryMapperTests()
            => _mapper = new CountryMapper();

        [Fact]
        public async Task AllCountriesCountAsync()
        {
            var result = await _mapper
                .MapAsync();

            result
                .Count()
                .Should()
                .Be(29);
        }

        [Theory]
        [InlineData(Countries.Amsterdam, 0)]
        [InlineData(Countries.Austria, 1)]
        [InlineData(Countries.Belgium, 2)]
        [InlineData(Countries.Bulgaria, 3)]
        [InlineData(Countries.Croatia, 4)]
        [InlineData(Countries.Cyprus, 5)]
        [InlineData(Countries.Czechia, 6)]
        [InlineData(Countries.Denmark, 7)]
        [InlineData(Countries.Estonia, 8)]
        [InlineData(Countries.Finland, 9)]
        [InlineData(Countries.France, 10)]
        [InlineData(Countries.Germany, 11)]
        [InlineData(Countries.Greece, 12)]
        [InlineData(Countries.Hungary, 13)]
        [InlineData(Countries.Ireland, 14)]
        [InlineData(Countries.Latvia, 15)]
        [InlineData(Countries.Lithuania, 16)]
        [InlineData(Countries.Italy, 17)]
        [InlineData(Countries.Luxembourg, 18)]
        [InlineData(Countries.Malta, 19)]
        [InlineData(Countries.Netherlands, 20)]
        [InlineData(Countries.Poland, 21)]
        [InlineData(Countries.Portugal, 22)]
        [InlineData(Countries.Romania, 23)]
        [InlineData(Countries.Slovakia, 24)]
        [InlineData(Countries.Slovenia, 25)]
        [InlineData(Countries.Spain, 26)]
        [InlineData(Countries.Sweden, 27)]
        [InlineData(Countries.UnitedKingdom, 28)]
        public async Task AllCountriesAsEnumTypeAsync(Countries country, int index)
        {
            var countries = await _mapper
                .MapAsync();

            var result = countries
                .ElementAt(index);

            result.EnglishName
                .Trim()
                .Should()
                .Be(country.ToString());
        }

        [Theory]
        [InlineData(0, "Amsterdam")]
        [InlineData(1, "Austria")]
        [InlineData(2, "Belgium")]
        [InlineData(3, "Bulgaria")]
        [InlineData(4, "Croatia")]
        [InlineData(5, "Cyprus")]
        [InlineData(6, "Czechia")]
        [InlineData(7, "Denmark")]
        [InlineData(8, "Estonia")]
        [InlineData(9, "Finland")]
        [InlineData(10, "France")]
        [InlineData(11, "Germany")]
        [InlineData(12, "Greece")]
        [InlineData(13, "Hungary")]
        [InlineData(14, "Ireland")]
        [InlineData(15, "Latvia")]
        [InlineData(16, "Lithuania")]
        [InlineData(17, "Italy")]
        [InlineData(18, "Luxembourg")]
        [InlineData(19, "Malta")]
        [InlineData(20, "Netherlands")]
        [InlineData(21, "Poland")]
        [InlineData(22, "Portugal")]
        [InlineData(23, "Romania")]
        [InlineData(24, "Slovakia")]
        [InlineData(25, "Slovenia")]
        [InlineData(26, "Spain")]
        [InlineData(27, "Sweden")]
        [InlineData(28, "UnitedKingdom")]
        public async Task AllCountriesEnglishNameAsync(int index, string englishName)
        {
            var countries = await _mapper
                .MapAsync();

            var result = countries
                .ElementAt(index);

            result.EnglishName
                .Should()
                .Be(englishName);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "AUT")]
        [InlineData(2, "BEL")]
        [InlineData(3, "BGR")]
        [InlineData(4, "HRV")]
        [InlineData(5, "CYP")]
        [InlineData(6, "CZE")]
        [InlineData(7, "DNK")]
        [InlineData(8, "EST")]
        [InlineData(9, "FIN")]
        [InlineData(10, "FRA")]
        [InlineData(11, "DEU")]
        [InlineData(12, "GRC")]
        [InlineData(13, "HUN")]
        [InlineData(14, "IRL")]
        [InlineData(15, "LVA")]
        [InlineData(16, "LTU")]
        [InlineData(17, "ITA")]
        [InlineData(18, "LUX")]
        [InlineData(19, "MLT")]
        [InlineData(20, "NLD")]
        [InlineData(21, "POL")]
        [InlineData(22, "PRT")]
        [InlineData(23, "ROU")]
        [InlineData(24, "SVK")]
        [InlineData(25, "SVN")]
        [InlineData(26, "ESP")]
        [InlineData(27, "SWE")]
        [InlineData(28, "GBR")]
        public async Task AllCountriesThreeLetterISOAsync(int index, string threeLetterISO)
        {
            var countries = await _mapper
                .MapAsync();

            var result = countries
                .ElementAt(index);

            result.ThreeLetterISO
                .Should()
                .Be(threeLetterISO);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "AT")]
        [InlineData(2, "BE")]
        [InlineData(3, "BG")]
        [InlineData(4, "HR")]
        [InlineData(5, "CY")]
        [InlineData(6, "CZ")]
        [InlineData(7, "DK")]
        [InlineData(8, "EE")]
        [InlineData(9, "FI")]
        [InlineData(10, "FR")]
        [InlineData(11, "DE")]
        [InlineData(12, "GR")]
        [InlineData(13, "HU")]
        [InlineData(14, "IE")]
        [InlineData(15, "LV")]
        [InlineData(16, "LT")]
        [InlineData(17, "IT")]
        [InlineData(18, "LU")]
        [InlineData(19, "MT")]
        [InlineData(20, "NL")]
        [InlineData(21, "PL")]
        [InlineData(22, "PT")]
        [InlineData(23, "RO")]
        [InlineData(24, "SK")]
        [InlineData(25, "SI")]
        [InlineData(26, "ES")]
        [InlineData(27, "SE")]
        [InlineData(28, "GB")]
        public async Task AllCountriesTwoLetterISOAsync(int index, string twoLetterISO)
        {
            var countries = await _mapper
                .MapAsync();

            var result = countries
                .ElementAt(index);

            result.TwoLetterISO
                .Should()
                .Be(twoLetterISO);
        }

        [Theory]
        [InlineData("Amsterdam", Countries.Amsterdam)]
        [InlineData("Austria", Countries.Austria)]
        [InlineData("Belgium", Countries.Belgium)]
        [InlineData("Bulgaria", Countries.Bulgaria)]
        [InlineData("Croatia", Countries.Croatia)]
        [InlineData("Cyprus", Countries.Cyprus)]
        [InlineData("Czechia", Countries.Czechia)]
        [InlineData("Denmark", Countries.Denmark)]
        [InlineData("Estonia", Countries.Estonia)]
        [InlineData("Finland", Countries.Finland)]
        [InlineData("France", Countries.France)]
        [InlineData("Germany", Countries.Germany)]
        [InlineData("Greece", Countries.Greece)]
        [InlineData("Hungary", Countries.Hungary)]
        [InlineData("Ireland", Countries.Ireland)]
        [InlineData("Latvia", Countries.Latvia)]
        [InlineData("Lithuania", Countries.Lithuania)]
        [InlineData("Italy", Countries.Italy)]
        [InlineData("Luxembourg", Countries.Luxembourg)]
        [InlineData("Malta", Countries.Malta)]
        [InlineData("Netherlands", Countries.Netherlands)]
        [InlineData("Poland", Countries.Poland)]
        [InlineData("Portugal", Countries.Portugal)]
        [InlineData("Romania", Countries.Romania)]
        [InlineData("Slovakia", Countries.Slovakia)]
        [InlineData("Slovenia", Countries.Slovenia)]
        [InlineData("Spain", Countries.Spain)]
        [InlineData("Sweden", Countries.Sweden)]
        [InlineData("UnitedKingdom", Countries.UnitedKingdom)]
        public async Task MapCountryEnumFromEnglishNameAsync(string englishName, Countries country)
        {
            var result = await _mapper
                .MapAsync(englishName);

            result
                .Should()
                .Be(country);
        }

        [Theory]
        [InlineData("AUT", Countries.Austria)]
        [InlineData("BEL", Countries.Belgium)]
        [InlineData("BGR", Countries.Bulgaria)]
        [InlineData("HRV", Countries.Croatia)]
        [InlineData("CYP", Countries.Cyprus)]
        [InlineData("CZE", Countries.Czechia)]
        [InlineData("DNK", Countries.Denmark)]
        [InlineData("EST", Countries.Estonia)]
        [InlineData("FIN", Countries.Finland)]
        [InlineData("FRA", Countries.France)]
        [InlineData("DEU", Countries.Germany)]
        [InlineData("GRC", Countries.Greece)]
        [InlineData("HUN", Countries.Hungary)]
        [InlineData("IRL", Countries.Ireland)]
        [InlineData("LVA", Countries.Latvia)]
        [InlineData("LTU", Countries.Lithuania)]
        [InlineData("ITA", Countries.Italy)]
        [InlineData("LUX", Countries.Luxembourg)]
        [InlineData("MLT", Countries.Malta)]
        [InlineData("NLD", Countries.Netherlands)]
        [InlineData("POL", Countries.Poland)]
        [InlineData("PRT", Countries.Portugal)]
        [InlineData("ROU", Countries.Romania)]
        [InlineData("SVK", Countries.Slovakia)]
        [InlineData("SVN", Countries.Slovenia)]
        [InlineData("ESP", Countries.Spain)]
        [InlineData("SWE", Countries.Sweden)]
        [InlineData("GBR", Countries.UnitedKingdom)]
        public async Task MapCountryEnumFromThreeLetterISOAsync(string threeLetterISO, Countries country)
        {
            var result = await _mapper
                .MapAsync(threeLetterISO);

            result
                .Should()
                .Be(country);
        }

        [Theory]
        [InlineData("AT", Countries.Austria)]
        [InlineData("BE", Countries.Belgium)]
        [InlineData("BG", Countries.Bulgaria)]
        [InlineData("HR", Countries.Croatia)]
        [InlineData("CY", Countries.Cyprus)]
        [InlineData("CZ", Countries.Czechia)]
        [InlineData("DK", Countries.Denmark)]
        [InlineData("EE", Countries.Estonia)]
        [InlineData("FI", Countries.Finland)]
        [InlineData("FR", Countries.France)]
        [InlineData("DE", Countries.Germany)]
        [InlineData("GR", Countries.Greece)]
        [InlineData("HU", Countries.Hungary)]
        [InlineData("IE", Countries.Ireland)]
        [InlineData("LV", Countries.Latvia)]
        [InlineData("LT", Countries.Lithuania)]
        [InlineData("IT", Countries.Italy)]
        [InlineData("LU", Countries.Luxembourg)]
        [InlineData("MT", Countries.Malta)]
        [InlineData("NL", Countries.Netherlands)]
        [InlineData("PL", Countries.Poland)]
        [InlineData("PT", Countries.Portugal)]
        [InlineData("RO", Countries.Romania)]
        [InlineData("SK", Countries.Slovakia)]
        [InlineData("SI", Countries.Slovenia)]
        [InlineData("ES", Countries.Spain)]
        [InlineData("SE", Countries.Sweden)]
        [InlineData("GB", Countries.UnitedKingdom)]
        public async Task MapCountryEnumFromTwoLetterISOAsync(string twoLetterISO, Countries country)
        {
            var result = await _mapper
                .MapAsync(twoLetterISO);

            result
                .Should()
                .Be(country);
        }

        [Theory]
        [InlineData("NoneExistingCountry")]
        [InlineData("AliBabAndtheFortyThievs")]
        [InlineData("United States")]
        public async Task ThrowsNotSupportedExceptionAsync(string country)
        {
            Func<Task> action = async () => await _mapper
                .MapAsync(country);

            var exception = await action
                .Should()
                .ThrowAsync<NotSupportedException>();

            exception
                .WithMessage($"Unknown '{country}' as country or is not supported.");
        }

        [Theory]
        [InlineData("US")]
        [InlineData("CA")]
        [InlineData("RU")]
        public async Task ThrowsNotSupportedExceptionAsValidTwoLetterIsoAsync(string twoLetterISO)
        {
            Func<Task> action = async () => await _mapper
                .MapAsync(twoLetterISO);

            var exception = await action
                .Should()
                .ThrowAsync<NotSupportedException>();

            exception
                .WithMessage($"Unknown '{twoLetterISO}' as country or is not supported.");
        }

        [Theory]
        [InlineData("USA")]
        [InlineData("CAN")]
        [InlineData("RUS")]
        public async Task ThrowsNotSupportedExceptionAsValidThreeLetterIsoAsync(string threeLetterISO)
        {
            Func<Task> action = async () => await _mapper
                .MapAsync(threeLetterISO);

            var exception = await action
                .Should()
                .ThrowAsync<NotSupportedException>();

            exception
                .WithMessage($"Unknown '{threeLetterISO}' as country or is not supported.");
        }

        [Theory]
        [InlineData("AA")]
        [InlineData("BX")]
        [InlineData("CB")]
        [InlineData("XX")]
        [InlineData("YY")]
        [InlineData("ZZ")]
        public async Task ThrowsArgumentExceptionAsInvalidTwoLetterISOAsync(string twoLetterISO)
        {
            Func<Task> action = async () => await _mapper
                .MapAsync(twoLetterISO);

            var exception = await action
                .Should()
                .ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData("AAA")]
        [InlineData("XXX")]
        [InlineData("YYY")]
        [InlineData("ZZZ")]
        public async Task ThrowsNotSupportedExceptionAsInvalidThreeLetterISOAsync(string threeLetterISO)
        {
            Func<Task> action = async () => await _mapper
                .MapAsync(threeLetterISO);

            var exception = await action
                .Should()
                .ThrowAsync<NotSupportedException>();

            exception
                .WithMessage($"Unknown '{threeLetterISO}' as country or is not supported.");
        }
    }
}