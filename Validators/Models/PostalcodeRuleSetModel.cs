using Validators.Interfaces;

namespace Validators.Models
{
    public struct PostalcodeRuleSetModel
        : IRuleSet
    {

        public string RegexPattern { get; set; }

        public string DisplayFormat { get; set; }

        public string Example { get; set; }

        public string Prefix { get; set; }
    }
}
