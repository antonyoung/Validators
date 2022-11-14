using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Console.Client.Enums;

namespace AntonYoung.Validators.Console.Client.Models
{
    internal class ValidatorModel
    { 
        internal Applications Application { get; set; }
        internal Countries Country { get; set; } = Countries.Netherlands;
        internal Formatters Formatter { get; set; } = Formatters.None;
        internal string Replace { get; set; } = string.Empty;
        internal string Value { get; set; } = string.Empty;
    }
}
