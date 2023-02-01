[![Build Status](https://dev.azure.com/antonyoung/c05894b0-db29-4558-b7c4-af27777e447d/_apis/build/status/4?branchName=master)](https://dev.azure.com/antonyoung/c05894b0-db29-4558-b7c4-af27777e447d/_apis/build/status/4?branchName=master)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)
## Postal code and IBAN validator and formatter (C#) .NET 7.x

Validates and formats European postal codes and International Bank Account Numbers ( IBAN )

### Intro

Started this little project in my spare time, for the reason that I noticed today ( 28 August 2019 ) at a project. 
That validation of Dutch postal codes is barely or none existent. 
The database contains postal codes as "1234", "1234aB", "1234-Ab", "1234abc", "1234-aBc", "xxxx" and so on, 
while only "[1-9]NNN AA" is valid as Dutch postal code. 
Working with corrupt data is not helping? As you have to work all around this.
Clean code, performance or just as SOLID principles, or just waisting my time?

As thought? This all can be done much easier, cleaner, more professional and so on.
As decision I started this project as a nice example. For me as? Additional bonus to get started with GitHub, Azure DevOps, and as open source project. 

This repository works, it's fast and it's reliable, extendable! 
As solution is really simple as long you know your regular expressions!
In the meantime as additional feature I added also an European IBAN validator "bit more complex, compared to postal code validations." 
As rules, all European countries, except is AlphaNumeric postal codes as only "UK and Ierland are they correctly validated?" 
Still have to think about how to implement formatters as postal code, or as IBAN? Implementation is there.

### Description

As a simple but as engine as a fast European postal code or as a iban validator with simplistic formatters. 
What could be easily extended to add additional countries around the world or to have different types of formatting.

**Technical:** 
As engine regular expressions are used with captured group names, for validation, formatting and sanity check ( iban.)
All countries as validation space or with hyphen or without prefix
Are all validated correctly, and will be automatically formatted in the Correct standardisation format in case off with space, hyphen or prefix. 
There's one exception as postal code prefix Finland has two prefixes FI- / AX- without prefix FI- is choosen as default.
Default country as for now is Netherlands. This can be easily defaulted as one liner change in concerned Validator classes.

**Note:**
Validator engine does not validate, if it's an existing postal code or iban!
It only validates as according rules for given country.

**References:**
Used the following website [publications.europa.eu](http://publications.europa.eu/code/en/en-390105.htm), as postal code rules and as guide lines in Europe. 
Source code works for all European countries as given on this website.
Not sure how valid the regular expressions are for the Alpha Numeric postal codes for the countires Ireland and UK?
I admit I just copied this from internet, I am not sure how this system works. 

Used the following website [en.wikipedia.org](https://en.wikipedia.org/wiki/International_Bank_Account_Number) as iban rules and as guide lines in Europe.
Not sure about Ierland, for now it also works as is, maybe the difference is between Republic vs Nothern Ierland?
 
### Prerequisites

```
* C# .NET Standard 2.1	// => Nuget-Packages
* C# .NET Core 7.0	// => Validators.Tests (xUnit) * With as total 372 tests => ~150 ms as test set.
```

### Dependency Injection ( DI )

* **As from semantic versioning 0.1.0-[AAAA], the validators are dependend on dependency injection ( DI )**

```csharp
// => add following to your IServiceCollection, named here as services.

// => as IPostalcodeValidator
services.AddTransient<IPostalcodeModel, PostalcodeModel>();
services.AddTransient<IPostalcodeValidator, PostalcodeValidator>();

// => as IIbanValidator
services.AddTransient<IIbanModel, IbanModel>();
services.AddTransient<IIbanValidator, IbanValidator>();
```

### Code examples ( PostalcodeValidator )

* **Constructor dependency injection ( DI )**
```csharp
public class MyClass
{
	private readonly IPostalcodeValidator _postalcodeValidator;

	public MyClass(IPostalcodeValidator postalcodeValidator)
		=> _postalcodeValidator = postalcodeValidator;
}
```

* **Happy flow ( with default country == Countries.Netherlands )**
```csharp
bool isValid = _postalcodeValidator
	.TryValidate("1062GD", out string result);

// => isValid = true
// => result  = "1062 GD"
```
* **Or as** 
```csharp
var test = _postalcodeValidator 
	.TryValidate("1062GD", Countries.Netherlands, out string result);

test.IsValid;       // => true					
test.ErrorMessage   // => null
result              // => "1062 GD"
```
* **Unhappy flow ( has leading zero )**
```csharp
var test = _postalcodeValidator 
	.TryValidate("0162GD", Countries.Netherlands, out string result);

test.IsValid;       // => false					
test.ErrorMessage   // => "Postal code \"0162GD\" is not valid. Use as example \"1234 AB\"."
result;             // => "0162GD"
```

### Code examples ( PostalcodeValidator with Formatters )

* **Use with Formatters.WhiteSpaces as Replace WhiteSpaces )**
* Example: In case there's any white space, and we don't want any white spaces as result? This formatter removes all white space(s) from result
```csharp
bool isValid = _postalcodeValidator
	.TryValidate("1062 GD", Countries.Netherlands, Formatters.WhiteSpaces, out string result); 

// => isValid = true
// => result  = "1062GD"
```
* **Or as** 
```csharp
var test = _postalcodeValidator 
	.TryValidate("1062 GD", Countries.Netherlands, out string result);

test.IsValid;       // => true					
test.ErrorMessage   // => null
result              // => "1062GD"
```

### Code examples ( PostalcodeValidator with Formatters and relace value )

* **Use with Formatters.WhiteSpaces as No Spaces, with replace value "-" )**
* Example: In case there's are any white spaces, and we want to replace any white space in result? 
The formatter replaces all white space(s) with the replace value in result.
* NOTE: Using replace value should be only used as, how we want to represent the result.
By using replace the result will be invalid as result.
```csharp
bool isValid = _postalcodeValidator
	.TryValidate("1062GD", Countries.Netherlands, Formatters.WhiteSpaces, "-", out string result); 

// => isValid = true
// => result  = "1062-GD"
```
* **Or as** 
```csharp
var test = _postalcodeValidator 
	.TryValidate("1062 GD", Countries.Netherlands, Formatters.WhiteSpaces, "-", out string result);

test.IsValid;       // => true					
test.ErrorMessage   // => null
result              // => "1062-GD"
```

### Code examples ( IbanValidator )

* **Constructor dependency injection ( DI )**
```csharp
public class MyClass
{
	private readonly IIbanValidator _ibanValidator;

	public MyClass(IIbanValidator ibanValidator)
		=> _ibanValidator = ibanValidator;
}
```

* **Happy flow**
```csharp
bool isValid = _ibanValidator
	.TryValidate("NL71INGB1320949010", out string result); 

// => isValid = true
// => result  = "NL71 INGB 1320 9490 10"
```
* **Or as** 
```csharp
var test = _ibanValidator
	.TryValidate("NL71INGB1320949010", out string result);

test.IsValid;			// => true					
test.ErrorMessage		// => null
test.AccountNumber		// => "1320949010"
test.Country			// => Countries.Netherlands
test.CheckDigits		// => 71
Test.NationalBankCode	        // => "INGB"
test.NationalBranchCode	        // => null
test.NationalCheckDigit	        // => null
result:                         // => "NL71 INGB 1320 9490 10"
```

### Code examples ( IbanValidator with Formatters and replace )

* **Example: Replace whitespaces with "."**
```csharp
bool isValid = _ibanValidator
	.TryValidate("NL71INGB1320949010", Formatters.WhiteSpace, ".", out string result); 
   
// => isValid = true
// => result  = "NL71.INGB.1320.9490.10"
```
* **Or as** 
```csharp
var test = _ibanValidator 
	.TryValidate("NL71INGB1320949010", Formatters.WhiteSpace, ".", out string result);

test.IsValid;			// => true					
test.ErrorMessage		// => null
test.AccountNumber		// => "1320949010"
test.Country			// => Countries.Netherlands
test.CheckDigits		// => 71
Test.NationalBankCode	        // => "INGB"
test.NationalBranchCode	        // => null
test.NationalCheckDigit	        // => null
result:                         // => "NL71.INGB.1320.9490.10"
```

## Authors

* **Anton Young** - *Initial work*

## License

This project is licensed for now under the GNU General Public License (GPL) License Lv3 - see the [LICENSE.md](LICENSE.md) file for details
It will be licenced as [Mit](https://github.com/git/git-scm.com/blob/main/MIT-LICENSE.txt) in case sufficient support from open source community.

### Acknowledgments
See also issues: [is:issue is:open check in:title](https://github.com/antonyoung/postalcode/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aopen+check+in%3Atitle)

### Additional features
See also issues: [is:issue is:open feature in:title](https://github.com/antonyoung/postalcode/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aopen+feature+in%3Atitle+)
