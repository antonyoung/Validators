using AntonYoung.Validators.Postalcode.Infrastructure;

namespace AntonYoung.Validators.Postalcode.Models
{
    public struct PostalcodeRuleSetModel : IRuleSet
    {
        public string RegexPattern { get; set; }
        public string DisplayFormat { get; set; }
        public string Example { get; set; }
        public string Prefix { get; set; }
    }
}