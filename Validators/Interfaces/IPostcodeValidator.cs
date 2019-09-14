using Validators.Formatters;


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


    /// <summary>
    ///     interface to be used for postal code business logic according to each country in Europe
    /// </summary>
    public interface IPostcodeValidator // : IPostcodeModel
    {


        /// <summary>
        ///     used as example postalcode of the set country. 
        /// </summary>
        string Example { get; }


        /// <summary>
        ///     used as message to the caller in case <seealso cref="IsValid"/> is false, else is string.Empty.
        /// </summary>
        string ErrorMessage { get; }


        /// <summary>
        ///     validates current input as postal code according to the set country.
        /// </summary>
        bool IsValid { get; }


        bool Validate(string value, out string result);


        bool Validate(string value, Countries country, out string result);


        bool Validate(string value, Countries country, PostcodeFormatters formatter, out string result);

    }
}
