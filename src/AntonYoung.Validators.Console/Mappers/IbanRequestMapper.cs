using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Domain.Abstractions.Requests;

namespace AntonYoung.Validators.Console.Mappers
{
    public interface IIbanRequestMapper
    {
        Task<IbanValidationRequest> MapAsync(ValidatorModel model);
    }

    public class IbanRequestMapper
        : IIbanRequestMapper
    {
        public Task<IbanValidationRequest> MapAsync(ValidatorModel model)
        {
            var result = new IbanValidationRequest
            {
                Value = model.Value,
                Formatter = model.Formatter,
                Replace = model.Replace
            };

            return Task
                .FromResult(result);
        }
    }
}