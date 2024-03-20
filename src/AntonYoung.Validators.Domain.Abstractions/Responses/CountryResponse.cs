namespace AntonYoung.Validators.Domain.Abstractions.Responses
{
    public class CountryResponse
    {
        public required string EnglishName { get; set; }
        public string ThreeLetterISO { get; set; } = string.Empty;
        public string TwoLetterISO { get; set; } = string.Empty;
    }
}