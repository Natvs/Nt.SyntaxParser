# Exporting a grammar
Exporting a grammar means getting a reusable or informational object (string, grammar code, new grammar file) out of a grammar.

## Why would I export?
This project is about loading a grammar for you to edit. If there are no ways of getting this grammar back, all your editions will be lost once the editor is closed.
Of course you can save it manually, but this project provides methods to handle it for you.

## Exportation methods
Before exporting a grammar, you must include `Nt.Syntax.Exportation` to extend the instances of grammar with exportation methods.
The exportation methods are:


|Method|Parameters|Return type|Comment|
|-----------------|-----------|-------|
|`ToString(mode)`|`mode` is one value of `ExportationMode`|string|More details below|
|`ToString()`||string|Same as above, but `mode` is always `ExportationMode.Original`. This method is accessible without importing `Nt.Syntax.Exportation`|

### Exporting as a string
Use the method `ToString(ExportationMode mode)` to export a grammar as a user-readable grammar.

The value of the `mode` parameter are:

|Name|Behavior|
|----|--------|
|ExportationMode.Original|Order of symbols and rules in the output grammar is the same as the one they are declared|
|ExportationMode.Alphabetical|Symbols and rules are reordered in the alphabetical order|
|ExportationMode.Grouped|Symbols with a common root in the name are grouped together. This is interesting once the grammar receives rules treatment and some symbols (terminals or non terminals) are extended from other.|

## How to use
Exportation methods are meant to be used in workflows where
1. A grammar is created from scratch or loaded from an existing grammar file
2. The grammar goes through various modifications, like grammar pre-treatment for further analysis
3. An exportation method is used to store the grammar or display it to a user

In your code, it will look something like this:

```csharp
using Nt.Syntax;
using Nt.Syntax.Exportation;

var parser = new SyntaxParser();
var grammar = parser.ParseFile("path/to/a/grammar/file");

--- Various modifications ---


-----------------------------

var grammar_string = grammar.ToString(ExportationMode.Grouped);
Console.WriteLine($"The resulting grammar is: \n{grammar_string}");
```