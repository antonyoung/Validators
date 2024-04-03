using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Domain.Abstractions.Responses;

namespace AntonYoung.Validators.Domain.Handlers
{
    public interface IFormattersHandler
    {
        Task<IEnumerable<FormatterResponse>> HandleAsync(CancellationToken cancellationToken);
    }

    public class FormattersHandler
        : IFormattersHandler
    {
        public async Task<IEnumerable<FormatterResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var result = new List<FormatterResponse>
            {
                new() 
                {
                    Formatter = Formatters.None,
                    Description = "None: keep as original with hyphen(s) and or whitespace(s)"
                },
                new()
                {
                    Formatter = Formatters.Hyphens,
                    Description = "Replace: hyphens \"-\" with given replace ( default = string.Empty )"
                },
                new()
                {
                    Formatter = Formatters.HyphensAndWhiteSpaces,
                    Description = "Replace: hyphens \"-\" and whitespaces \" \" with given replace ( default = string.Empty )"
                },
                new()
                {
                    Formatter = Formatters.WhiteSpaces,
                    Description = "Replace: whitespaces \" \" with given replace ( default = string.Empty )"
                }
            };

            return await Task
                .FromResult(result);
        }
    }
}
