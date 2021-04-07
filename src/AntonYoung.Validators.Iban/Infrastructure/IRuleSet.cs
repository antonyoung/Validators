namespace AntonYoung.Validators.Iban.Infrastructure
{
    /// <summary>
    ///     used as the internal logic to define the rule set of a country, exxpand this interface as you wish?
    ///     engine based on: <seealso cref="System.Text.RegularExpressions.Match.Groups"/>
    ///     formatting based on <seealso cref="System.Text.RegularExpressions.Match.Groups.Name"/>
    /// </summary>
    public interface IRuleSet
    {
        /// <summary>
        ///     used as the regular expression to do the match as <seealso cref="System.Text.RegularExpressions.Match.Groups"/>.
        ///     use this to your advantage in <seealso cref="DisplayFormat"/>.
        /// </summary>
        string RegexPattern { get; set; }

        /// <summary>
        ///     used as how to format the match group names?  
        ///     use: <Group.Name> as the string that has to be replaced.
        /// </summary>
        string DisplayFormat { get; set; }

        /// <summary>
        ///     used as an example string with provided <seealso cref="Formatters"/>.
        /// </summary>
        string Example { get; set; }
    }
}