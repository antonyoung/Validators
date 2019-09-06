[![Build Status](https://dev.azure.com/antonyoung/Validator/_apis/build/status/antonyoung.postalcode?branchName=master)](https://dev.azure.com/antonyoung/Validator/_build/latest?definitionId=3&branchName=master)
## Postcode validator and formatter (C#) .Net Core

Validates and formats European postcodes

### Intro

Started this little project in my spare time, because I noticed today (28 August 2019) at work that validation of Dutch postcodes is barely or none existent. 
The database contains postcodes as "1234", "1234aB", "1234-Ab", "1234abc", "1234-aBc", "xxxx" and so on, while only "[1-9]NNN AA" is valid. 
While the solution is really simple as long you know your regular expressions. 

### Description

This is a simple and fast European postcode validator and formatter. 
Could be easily extended to add countries around the world.

As validation regular expressions are used with group names. 
These group names of the regular expression are used for the posal code formatting. 
All countries with space or with hyphen or with prefix.
Are all validated correctly without it and will be automatically formatted in the correct way with space, hyphen or prefix. 
With one exception Finland has two prefixes FI- / AX- without prefix FI- is choosen as default.
Default country as expected is for now The Netherlands.    

**Note:** - it does not validate if it's an existing postal code!

Used the following website [publications.europa.eu](http://publications.europa.eu/code/en/en-390105.htm), as postal code rules and as guide lines in Europe. 
Source code works for all European countries as given on this website.
Not sure how valid the regular expressions are for the Alpha Numeric postcodes for the countires, Ireland and UK?
I admit I just copied this from internet, I am not sure how this system works. 
 
### Prerequisites
```
* C# .NET Core 2.2.0	  // => Validator
* C# .NET Core 2.2.0      // => Postcode.Tests (xUnit) * With 147 tests => < 2 ms per test.
```
### Code examples

* **Happy flow**
```csharp
bool isValid = new PostcodeValidator()
   .TryParse("1062GD", Countries.Netherlands, out string ressult); // => result = "1062 GD", isValid = true
```
* **Or as** 
```csharp
var test = new PostcodeValidator(); 
test.TryParse("1062GD", Countries.Netherlands, out string ressult);
test.IsValid;       // => true					
test.ErrorMessage   // => string.Empty
result              // => "1062 GD"
```
* **Unhappy flow ( has leading zero )**
```csharp
var test = new PostcodeValidator(); 
test.TryParse("0162GD", Countries.Netherlands, out string ressult);
test.IsValid;       // => false					
test.ErrorMessage   // => "Postal code \"0162GD\" is not valid. Example \"1234 AB\"."
result;             // => "0162GD"
```

## Authors

* **Anton Young** - *Initial work*

## License

This project is licensed under the GNU General Public License (GPL) License Lv3 - see the [LICENSE.md](LICENSE.md) file for details

### Acknowledgments

- [ ] Check		: Countries has leading zeros or not? ( currently all countries does not except leading zeros, and what about numbers at the right side? )
- [ ] Ireland	: Is this really the correct regular expression?
- [ ] UK		: Is this really the correct regular expression? * Hint as a solution: [stackoverflow](https://stackoverflow.com/questions/164979/regex-for-matching-uk-postcodes)	
 
### Additional features

- [ ] Implement: Github add code of conduct, contributing, issue templates and pull-requests => Change Readme as issues template.
- [ ] Add : Simplistic PostcodeFormatter, to format as default, no spaces or and hyphen / dashes.
- [ ] Convert	: Validators as net standard framework ( currently standard does not support System.Text.RegularExpression group.Name ) => definitly a want!
- [ ] Convert : Azure devops pipeline, use as Linux build with code coverage badge.
- [ ] Add : Build as Nuget packages, eventually deploy as Nuget Package.
- [ ] Convert	: Validators project as javascript library ( new github repository ) => Really nice to have.
- [ ] Add		: As extra functionality to check if it is an existing postal code or not, google maps? => nice to have, but it has to be for free.
- [ ] Add  : As new validator an IBAN validator and all it's features.
- [ ] Extend	: Add other countries outside of European Union. => nice to have 
- [ ] Extend	: This whole project, how to display full address information for given country. ( new github repository ) => definitally we need validation for this, if address is valid! )
