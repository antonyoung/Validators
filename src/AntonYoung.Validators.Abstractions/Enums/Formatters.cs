namespace AntonYoung.Validators.Abstractions.Enums
{
    /// <summary>
    ///     available formatters as replace formatters ( default = string.Empty )
    /// </summary>
    public enum Formatters
    {
        /// <summary>
        ///     none: keep as original with hyphen(s) and or whitespace(s)
        /// </summary>
        None,

        /// <summary>
        ///     replace: hyphens "-" with given replace ( default = string.Empty )
        /// </summary>
        Hyphens,

        /// <summary>
        ///     replace: hyphens "-" and whitespaces " " with given replace ( default = string.Empty )
        /// </summary>
        HyphensAndWhiteSpaces,

        /// <summary>
        ///     replace: whitespaces " " with given replace ( default = string.Empty )
        /// </summary>
        WhiteSpaces
    }
}