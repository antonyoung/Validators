using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Enums;

namespace AntonYoung.Validators.Console.Models
{
    internal class ValidatorModel
    {
        internal Applications Application { get; set; } = Applications.Post;
        internal string? Country { get; set; }
        internal Formatters Formatter { get; set; } = Formatters.None;
        internal string Replace { get; set; } = string.Empty;
        internal string Value { get; set; } = string.Empty;
        internal bool IsHelp { get; set; } = false;
    }
}