## Postal code validator and formatter (C#)

Validates and formats European postal codes

### Intro

Started this little project in my spare time, because I noticed today (28 August 2019) at work that validation of Dutch postal codes is barely or none existent. 
The database contains postal codes as "1234", "1234aB", "1234-Ab", "1234abc", "1234-aBc", "xxxx" and so on, while only "[1-9]NNN AA" is valid. 
While the solution is really simple as long you know your regular expressions. 

### Description

This is a simple and fast European postal code validator and formatter. 
Could be easily extended add countries around the world.

As validation regular expressions are used with group names. 
Group names of the regular expressions are used for the postal code formatting. 
All countries with space or with hyphen or with prefix.
Are all validated correctly without them and will be automatically formatted in the correct way with space, hyphen or prefix. 
With one exception Finland has two prefixes FI- / AX- without prefix FI- will be choosen as default.
Currently the default country as expected for now is The Netherlands.    

**Note:** - it does not validate if it's an existing postal code!

Used the following site, as postal code rules and as guide lines in Europe: http://publications.europa.eu/code/en/en-390105.htm
Current source code works for all European countries as given on this website.
Not sure how valid the regular expressions are for the Alpha Numeric postal codes for the countires, Ireland and UK?
I admit I just copied this from internet, I am not sure how this system works. 
 
### Prerequisites
```
* C# .NET Framework 4.7.2 // => PostalCode.Library
* C# .NET Core 2.2.0      // => PostalCode.Tests (xUnit) * With 149 tests => ~2 ms per test.
```
### Code examples

* **Happy flow**
```csharp
var formattedPostalCode = Library.PostalCode(Countries.Netherlands, "1062GD").ToString(); // => "1062 GD"
```
* **Or as** 
```csharp
var test = Library.PostalCode(Countries.Netherlands, "1062GD");
var isValid = test.IsValid;                 // => true					
var message = test.ErrorMessage             // => string.Empty
var formattedPostalCode = test.ToString();  // => "1062 GD"
```
* **Unhappy flow ( has leading zero )**
```csharp
var test = Library.PostalCode(Countries.Netherlands, "0162GD");
var isValid = test.IsValid;                 // => false					
var message = test.ErrorMessage             // => "Postal code \"0162GD\" is not valid. Example \"1234 AB\"."
var formattedPostalCode = test.ToString();  // => "0162GD"
```

## Authors

* **Anton Young** - *Initial work*

### License

This project is licensed under the GNU General Public License (GPL) License Lv3 - see the [LICENSE.md](LICENSE.md) file for details

### Acknowledgments

- [ ] Check		: Countries has leading zeros or not? ( currently all countries does not except leading zeros, and what about numbers at the right side? )
- [ ] Ireland	: Is this really the correct regular expression?
- [ ] UK		: Is this really the correct regular expression? * Hint as a solution: [stackoverflow](https://stackoverflow.com/questions/164979/regex-for-matching-uk-postcodes)	

### Additional features

- [ ] Extend	: Add other countries outside of European Union. => nice to have
- [ ] Add		: As functionality to check if is existing postal code or not?	=> nice to have, but for free.
- [ ] Convert	: PostalCode.Library => net standard framework ( currently does not support group.Name ) => definitly a want!
- [ ] Convert	: PostalCode.Library => javascript library ( new github repository ) => Really nice to have.
- [ ] Extend	: This whole project, how to display full address information for given country. ( new github repository ) => definitally we need validation for this, if address is valid! )   
