using System.Collections.Generic;

using Validators.Enums;
using Validators.Models;


namespace Validators.Interfaces
{

    /// <summary>
    ///     interface: used to define the postcode in memory internal logic as data source.
    /// </summary>as 
    public interface IPostcodeModel
    {

        /// <summary>
        ///     used as the total set to define the internal logic for each country available as data source. 
        ///     engine: based on <seealso cref="System.Text.RegularExpressions.Group"/>.
        /// </summary>
        /// <exception cref=System.ArgumentException">
        ///     thrown, when country is not found in defined data source.
        /// </exception>
        Dictionary<Countries, PostcodeRuleSetModel> Rules { get; }
    }
}
