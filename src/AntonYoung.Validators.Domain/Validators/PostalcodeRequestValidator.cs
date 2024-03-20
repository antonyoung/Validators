using AntonYoung.Validators.Domain.Abstractions.Requests;
using AntonYoung.Validators.Abstractions.Enums;

namespace AntonYoung.Validators.Domain.Validators
{
    public interface IPostalcodeRequestValidator
    {
        Task<IEnumerable<string>> ValidateAsync(PostalcodeValidaionRequest request, Countries country);
    }

    public class PostalcodeRequestValidator 
        : IPostalcodeRequestValidator
    {
        private readonly IList<string> _errorMessages;

        public PostalcodeRequestValidator() 
            => _errorMessages = new List<string>();

        public async Task<IEnumerable<string>> ValidateAsync(PostalcodeValidaionRequest request, Countries country)
        {
            if (string.IsNullOrWhiteSpace(request.Value))
                _errorMessages.Add("Value is obliged, to be able to do a postalcode validation.");

            if (country == Countries.Amsterdam)
                _errorMessages.Add("Amsterdam is not supported as country to be validated.");

            if (!Enum.IsDefined(typeof(Countries), country.ToString()))
                _errorMessages.Add($"{request.Country} is not supported as country to be validated.");

            if (!Enum.IsDefined(typeof(Formatters), request.Formatter.ToString()))
                _errorMessages.Add($"{request.Formatter} is not supported as formatter.");

            return await Task.FromResult(_errorMessages);
        }
    }
}