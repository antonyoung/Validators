using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Enums;

namespace AntonYoung.Validators.Console.Models
{
    public class ValidatorModel
    {
        public Applications Application { get; set; } = Applications.Post;
        public string? Country { get; set; }
        public Formatters Formatter { get; set; } = Formatters.None;
        public string Replace { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsHelp { get; set; } = false;
    }
}