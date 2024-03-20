using AntonYoung.Validators.Console.Models;
using AntonYoung.Validators.Domain.Abstractions.Requests;

namespace AntonYoung.Validators.Console.Mappers
{
    internal interface IPostalcodeRequestMapper
    {
        Task<PostalcodeValidaionRequest> MapAsync(ValidatorModel model);
    }

    internal class PostalcodeRequestMapper
        : IPostalcodeRequestMapper
    {
        public Task<PostalcodeValidaionRequest> MapAsync(ValidatorModel model)
        {
            var result = new PostalcodeValidaionRequest
            {
                Country = model.Country ?? string.Empty,
                Formatter = model.Formatter,
                Replace = model.Replace,
                Value = model.Value
            };

            return Task.FromResult(result);
        }
    }
}