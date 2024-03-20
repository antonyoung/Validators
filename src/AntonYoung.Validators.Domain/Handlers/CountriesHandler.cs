using AntonYoung.Validators.Domain.Abstractions.Responses;
using AntonYoung.Validators.Domain.Mappers;

namespace AntonYoung.Validators.Domain.Handlers
{
    public interface ICountriesHandler
    {
        Task<IEnumerable<CountryResponse>> HandleAsync(CancellationToken cancellationToken);
    }

    public class CountriesHandler
        : ICountriesHandler
    {
        private readonly ICountryMapper _mapper;

        public CountriesHandler(ICountryMapper mapper)
            => _mapper = mapper;

        public async Task<IEnumerable<CountryResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            return await _mapper.MapAsync();
        }
    }
}