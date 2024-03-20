using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Abstractions.Responses;
using System.Globalization;

namespace AntonYoung.Validators.Domain.Mappers
{
    public interface ICountryMapper
    {
        Task<IEnumerable<CountryResponse>> MapAsync();
        Task<Countries> MapAsync(string countryCode);
    }

    public class CountryMapper
        : ICountryMapper
    {
        public async Task<Countries> MapAsync(string countryCode)
        {
            int lenght = countryCode.Length;

            if (lenght > 3) 
            { 
                Enum.TryParse<Countries>(countryCode, ignoreCase: true, out var countryByName);

                return await Task.FromResult(countryByName);
            }

            if (lenght == 3)
            {
                var countries = await MapAsync();

                countryCode = countries
                    .SingleOrDefault(_ 
                        => _.ThreeLetterISO.Equals(countryCode, StringComparison.OrdinalIgnoreCase)
                    )?.TwoLetterISO ?? countryCode.Remove(2);
            }

            var region = new RegionInfo(countryCode);

            Enum.TryParse<Countries>(region.EnglishName.Replace(" ", string.Empty), ignoreCase: true, out var country);

            return await Task.FromResult(country);
        }

        public async Task<IEnumerable<CountryResponse>> MapAsync()
        {
            IList<CountryResponse> result = new List<CountryResponse>();

            var regions = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Select(_ => new RegionInfo(_.Name));

            var countries = Enum
                .GetValuesAsUnderlyingType(typeof(Countries))
                .Cast<Countries>();

            foreach (var country in countries) 
            {
                var englishName = country == Countries.UnitedKingdom
                    ? "United Kingdom"
                    : country.ToString();

                var region = regions
                    .FirstOrDefault(_ => _.EnglishName == englishName);

                result.Add
                (
                    new CountryResponse
                    {
                        EnglishName = country.ToString(),
                        ThreeLetterISO = region?.ThreeLetterISORegionName ?? string.Empty,
                        TwoLetterISO = region?.TwoLetterISORegionName ?? string.Empty,
                    }
                );
            }

            return await Task.FromResult(result);
        }
    }
}