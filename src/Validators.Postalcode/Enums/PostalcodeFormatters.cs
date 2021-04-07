namespace Validators.Postalcode.Enums
{
    /// <summary>
    ///     available formatters for postal codes.
    /// </summary>
    public enum PostalcodeFormatters
    {
        None,                       // none: keep as original with hyphen(s) and or whitespace(s). 
        Hyphens,                    // replace: hyphens with given replace ( default = string.Empty )
        HyphensAndWhiteSpaces,      // replace: hyphens and whitespaces with given replace ( default = string.Empty )
        WhiteSpaces,                // replace: whitespaces with given replace ( default = string.Empty )
    }
}
