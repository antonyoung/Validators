namespace AntonYoung.Validators.Abstractions.Constants
{
    /// <summary>
    ///     Constants used as replace values.
    /// </summary>
    public static class Replaces
    {
        public const string Default = "<{0}>";
        internal const string WhiteSpaces = @"\s+";
        internal const string Hyphens = @"\-+";
        internal const string HyphensAndWhiteSpaces = @"[\s+\-+]";
    }
}
