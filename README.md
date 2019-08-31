## Postal code validator and formatter (C#)

Validates and formats European postal codes

## Intro

Started this little project in my spare time, because I noticed today at work that validation of Dutch postal codes is barely or none existent. The database contains postal codes as "1234", "1234aB", "1234-Ab", "1234abc", "1234-aBc", "xxxx" and so on, while only "1NNN AA" is valid.

This is a simple and fast European postal code validator and formatter. Could easily be extended to add countries around the world.

As validation regular expressions are used with group names. For postal code formatting the group names of the regular expressions are used. All countries with space or with hyphen or with prefix, are all validated correctly without and will be automatically formatted in the correct way with space, hyphen or prefix with one exception Finland has two prefixes FI- / AX-. With out prefix FI- will be choosen. Currently the default country as expected for now is The Netherlands.    

**Note:** - it does not validate if it's an existing postal code!

Used the following site, as postal code rules and as guide lines in Europe: http://publications.europa.eu/code/en/en-390105.htm
Current source code works for all European countries as given on this website. Not sure how valid the regular expressions are for the Alpha Numeric postal codes countires Ireland and UK? I admit I just copied this from internet, I am not sure how this system works. 
 
### Prerequisites

* C# .NET Framework 4.7.2 // => PostalCode.Library
* C# .NET Core 2.2.0      // => PostalCode.Tests (xUnit) * Contians currently 149 tests => ~2 ms per test.

### Code examples

* **Happy flow**

```csharp
var formattedPostalCode = Library.PostalCode(Countries.Netherlands, "1062GD").ToString(); // => "1062 GD"
```

**Or as** 

```csharp
var test = Library.PostalCode(Countries.Netherlands, "162GD");
var formattedPostalCode = test.ToString();  // => "1062 GD"
var isValid = test.IsValid;                 // => true					
var message = test.ErrorMessage             // => string.Empty		
```

* **Unhappy flow	: ( has leading zero )**

```csharp
var test = Library.PostalCode(Countries.Netherlands, "0162GD");

var formattedPostalCode = test.ToString();  // => "0162GD"
var isValid = test.IsValid;                 // => false					
var message = test.ErrorMessage             // => "Postal code \"0162GD\" is not valid. Example \"1234 AB\"."		
```

## Authors

* **Anton Young** - *Initial work*

## License

This project is licensed under the GNU General Public License (GPL) License Lv3 - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

- [ ] Check	  : Leading zeros or not? ( currently does not except leading zeros, and what about countries with a right side? )
- [ ] Ireland	: Is this really the correct regular expression?
- [ ] UK		  : Is this really the correct regular expression? * Hint as a solution: https://stackoverflow.com/questions/164979/regex-for-matching-uk-postcodes	
- [ ] Extend	: Add other countries outside of European Union. ( nice to have )
- [ ] Add		  : As functionality to check if is existing postal code or not?	( nice to have )	
- [ ] Convert	: PostalCode.Library => .net standard framework ( currently does not support group.Name ) ( definitly a want )
- [ ] Convert	: PostalCode.Library => JavaScript library ( new github repository )
