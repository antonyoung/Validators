using Validators.Interfaces;


namespace Validators.Models
{
    public struct IbanRuleSetModel
        : IRuleSet
    {
        public string RegexPattern { get ; set; }

        public string DisplayFormat { get; set; }

        public string Example { get; set; }

        public string SanityFormat { get; set; }

        public int Length { get; set; }
    }
}
