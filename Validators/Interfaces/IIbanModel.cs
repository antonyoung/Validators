using System.Collections.Generic;
using Validators.Models;


namespace Validators.Interfaces
{

    /// <summary>
    ///     interface: used to define the iban in memory internal logic as data source.
    /// </summary>as 
    public interface IIbanModel
    {


        /// <summary>
        ///     used as the total set to define the internal logic for each country available as data source. 
        ///     engine: based on <seealso cref="System.Text.RegularExpressions.Group"/>.
        /// </summary>
        /// <exception cref=System.ArgumentException">
        ///     thrown, when country is not found in defined data set.
        /// </exception>
        Dictionary<Countries, IbanRuleSetModel> Rules { get; }
    }
}
