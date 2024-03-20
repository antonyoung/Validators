using AntonYoung.Validators.Abstractions.Enums;

namespace AntonYoung.Validators.Domain.Abstractions.Responses
{
    public class FormatterResponse
    {
        public required Formatters Formatter { get; set; }
        public required string Description { get; set; }
    }
}