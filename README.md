## Postal code validator and formatter (C#)

Validates and formats European postal codes

## Intro

Started this little project in my spare time, because I noticed today at work that validation of Dutch postal codes is barely or none existent. The database contains postal codes as "1234", "1234aB", "1234-Ab", "1234abc", "1234-aBc", "xxxx" and so on, while "1NNN AA" is valid.

This is a simple and fast European postal code validator and formatter. Could easily extended to add countries around the world.
As validation regular expressions with group names are used. For postal code formatting the group names of the regular expressions are used.

* **Note** - it does not validate if it's an existing postal code.

Used following site, postal code rules guide lines in Europe: http://publications.europa.eu/code/en/en-390105.htm

### Prerequisites

* C# .NET Framework 4.7.2
* C# .NET Core 2.2.0

### Code examples

* **Happy flow**

```csharp
var formattedPostalCode = Library.PostalCode(Countries.Netherlands, "1062GD").ToString(); // => "1062 GD"
```

* **Or** 

```csharp
var test = Library.PostalCode(Countries.Netherlands, "162GD");
var formattedPostalCode = test.ToString();  // => "1062 GD"
var isValid = test.IsValid; // => true					
var message = test.ErrorMessage // => string.Empty		
```

* **Unhappy flow	: ( has leading zero )**

```csharp
var test = Library.PostalCode(Countries.Netherlands, "0162GD");

var formattedPostalCode = test.ToString();  // => "0162GD"
var isValid = test.IsValid; // => false					
var message = test.ErrorMessage // => "Postal code \"0162GD\" is not valid. Example \"1234 AB\"."		
```

## Authors

* **Anton Young** - *Initial work*

## License

This project is licensed under the GNU General Public License (GPL) License Lv3 - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

- [ ] Check	  : Leading zeros or not? ( currently does not except leading zeros. )
- [ ] Ireland	: Is this really the correct regular expression?
- [ ] UK		  : Is this really the correct regular expression? * Hint: https://stackoverflow.com/questions/164979/regex-for-matching-uk-postcodes	
- [ ] Extend	: Add other countries outside of European Union.
- [ ] Add		  : As functionality to check if is existing postal code or not?		
- [ ] Convert	: PostalCode.Library => .net standard framework ( currently does not support group.Name )
- [ ] Convert	: PostalCode.Library => JavaScript library ( new github repository )
