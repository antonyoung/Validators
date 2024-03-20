using AntonYoung.Validators.Abstractions.Enums;

namespace AntonYoung.Validators.Domain.Abstractions.Requests
{
    public class PostalcodeValidaionRequest
    {
        public string Country { get; set; } = string.Empty;
        public Formatters Formatter { get; set; } = Formatters.None;
        public string Replace { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}