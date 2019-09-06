namespace Validators.Interfaces
{
    // => http://publications.europa.eu/code/en/en-390105.htm

    public enum Countries
    {
        Amsterdam,      // => argument exception
        Austria,
        Belgium,
        Bulgaria,
        Croatia,        // The postal code must be preceded by ‘HR-’
        Cyprus,
        Czechia,        // There is a space between the third and fourth figures
        Denmark,
        Estonia,
        Finland,        // The postal code must be preceded by ‘FI-’ (or by ‘AX-’ for the Åland Islands).
        France,
        Germany,
        Greece,         // There is a space between the third and fourth figures
        Hungary,
        Ireland,
        Latvia,         // The postal code must be preceded by ‘LV-’.
        Lithuania,      // The postal code must be preceded by ‘LT-’
        Italy,
        Luxembourg,     // The postal code must be preceded by ‘L-’
        Malta,          // with a space between the letters and figures
        Netherlands,    // There is a space between the figures and letters.
        Poland,         // There is a hyphen (‐) ‒ between the second and third figures.
        Portugal,       // There is a hyphen between the fourth and fifth figures.
        Romania,
        Slovakia,       // There is a space between the third and fourth figures.
        Slovenia,       // The postal code must be preceded by ‘SI-’.
        Spain,
        Sweden,         // The postal code must be preceded by ‘SE-’. There is a space between the third and fourth figures.
        UnitedKingdom   // A space separates the first block (2 to 4 alphanumeric characters) from the second block (3 characters which are always in the order: figure letter letter).
    }


    public enum RemoveFormatter
    {
        Default,
        Hyphens,
        HyphensAndWhiteSpaces,
        WitheSpaces,
    }

    /// <summary>
    ///     interface to be used for postal code business logic according to each country in Europe
    /// </summary>
    public interface IPostcodeValidator
    {

        /// <summary>
        ///     used as constructor argument to set the postalcode, default is string.Empty.
        ///     public set to set a new postal code of the already setted country.
        /// </summary>
        //string Input { set; }


        /// <summary>
        ///     used as constructor argument to set the postal code business logic of a certain country,  
        ///     public get is available for the caller in case needs to know, which country?
        /// </summary>
        //Countries Country { get; }


        /// <summary>
        ///     used as example postalcode of the set country. 
        /// </summary>
        string Example { get; }


        /// <summary> 
        ///     used as formatted postal code of the set country without any spaces, if any?
        /// </summary>
        //string NoWhiteSpaces { get; }


        /// <summary>
        ///     used as message to the caller in case <seealso cref="IsValid"/> is false, else is string.Empty.
        /// </summary>
        string ErrorMessage { get; }


        /// <summary>
        ///     validates current input as postal code according to the set country.
        /// </summary>
        bool IsValid { get; }


        /// <summary>
        ///     validates and formats the postal code, that is expected from the selected country.
        /// </summary>
        /// <returns>
        ///     the formatted postal code of the selected country <seealso cref="IsValid"/> is true.
        ///     else return the postal code as given <seealso cref="Input"/>
        /// </returns>
        //string ToString();


        bool TryParse(string value, out string result);

        bool TryParse(string value, Countries country, out string result);

        bool TryParse(string value, Countries country, RemoveFormatter formatter, out string result);

    }
}
