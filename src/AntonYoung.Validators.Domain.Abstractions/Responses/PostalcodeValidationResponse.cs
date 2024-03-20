using AntonYoung.Validators.Abstractions.Enums;

namespace AntonYoung.Validators.Domain.Abstractions.Responses
{
    public class PostalcodeValidationResponse
    {
        public required string Result { get; set; }
        public bool IsValid { get; set; }
        public Countries? Country { get; set; }
        public string? ErrorMessage { get; set; }
    }
}