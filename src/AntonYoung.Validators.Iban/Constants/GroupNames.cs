namespace AntonYoung.Validators.Iban.Constants
{
    /// <summary>
    ///     Constants used as Captured Group Names <see cref="System.Text.RegularExpressions"/>
    /// </summary>
    internal static class GroupNames
    {
        internal const string Account = "account";
        internal const string AccountType = "bban";
        internal const string Bank = "bank";
        internal const string Branch = "branch";
        internal const string CheckDigits = "checksum";
        internal const string Country = "country";
        internal const string NationalCheckDigit = "ncheck";
        internal const string WhiteSpace = "whitespace";
    }
}