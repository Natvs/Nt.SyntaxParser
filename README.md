# SyntaxParser

- [Introduction](#introduction)
- [Advantages of using Nt.SyntaxParser](#advantages-of-using-ntsyntaxparser)
- [Prerequisites](#prerequisites)
	- [Install the Nt.SyntaxParser package](#install-the-ntsyntaxparser-package)
	- [Define the grammar syntax](#define-the-grammar-syntax)
- [Using the syntax parser](#using-the-syntax-parser)
- [Additional features of the parser](#additional-features-of-the-parser)
	- [Extend the grammar syntax with custom blocks](#extend-the-grammar-syntax-with-custom-blocks)
	- [Store custom payload into an element of the grammar](#store-custom-payload-into-an-element-of-the-grammar)
	- [Export the grammar to a string](#export-the-grammar-to-a-string)
	- [Specify a custom symbol factory for parsing](#specify-a-custom-symbol-factory-for-parsing)
- [Exceptions](#exceptions)

## Introduction

Do you ever had trouble manually parsing a file and looking for an information inside it ?
Or do you want to create your own language and need to interpret it ?

This project is the first step to that purpose.
Define a grammar for the language you want to parse, and the syntax parser will take care of parsing it for you.

## Advantages of using Nt.SyntaxParser

- Block syntax designed to be light and efficient
- Define regular expressions to avoid troubles with complex derivation rules
- Load a grammar, edit it and save it back to a file
- Extend the syntax to include your own configurations
- Store custom payload into an element of the grammar
- Import external grammars for use of external elements
- Cross-application support for grammar files
- Ready for analysis with `Nt.SyntaxAnalyzer`

## Prerequisites

### Install the Nt.SyntaxParser package

Install the NuGet package `Nt.SyntaxParser` in your project from Nuget.org:

 ```powerShell
 dotnet add package Nt.SyntaxParser --version 2.0.0
 ```

or add the following line to your project file:
```xml
<PackageReference Include="Nt.SyntaxParser" Version="2.0.0" />
```

### Create the grammar used to parse a file

A grammar contains the following elements:
- A set of terminal symbols (constant strings)
- A set of non-terminal symbols (symbols to derive)
- A set of derivation rules (rules to derive non-terminals into sequences of terminals and non-terminals)
- A set of regular expression rules (rules to derive non-terminals into regular expressions)
- An axiom (the starting non-terminal symbol for derivations)

The syntax parser uses a grammar file to create a grammar structure.
A grammar file is a text file that describes the grammar of a language. 
The syntax of the grammar file is described in the [grammar file syntax documentation](Doc/Grammar.md).

## Using the syntax parser
To use the syntax parser, you first need to create an instance of the `SyntaxParser` class. There are two ways of parsing a grammar file: from a string or from a file.

To parse from a string
```csharp
using Nt.Syntax;
using Nt.Syntax.Structures;

var generator = new SyntaxParser();
Grammar grammar = generator.ParseString(grammarString);
```

To parse from a file
```csharp
using Nt.Syntax;
using Nt.Syntax.Structures;

var generator = new SyntaxParser();
Grammar grammar = generator.ParseFile(filePath);
```

## Additional features of the parser

### Extend the grammar syntax with custom blocks

Whether you want to add more information to the grammar or add custom configurations, you can add new blocks to the grammar file on top of the 5 existing ones.

For example, it is possible to specify that some terminals of the grammar are considered operators within a custom block.
```
OPERATORS (
	+ - * /
)
```
Of course these can be hard coded, but creating a new block allow users to specify their own operators without having to modify the code of the parser.

This blocks extension supports cross-applications usage, meaning that multiple applications can share the same grammar file while including their own specific information.
Find more information about this feature in the [extensions documentation](Doc/Extensions.md).

### Store custom payload into an element of the grammar

Do you need to add external information to an element of the grammar, but don't know how to store it ?
The `payload` feature is meant to link elements like terminals or rules with additional information, like tags, conditions or any other information.

With the `payload` feature activated, there is no need to manage external data structures as the data is contained inside the grammar itself.
The payload accepts any structure of data, so it's up to you to decide which data you store.

The whole feature, and how to activate it, is described in the [payload documentation](Doc/Payload.md).

### Export the grammar to a string

Once a grammar is created, you can choose to export it to a string.
The exported string can be of different formats, depending on your needs.

- Export to a user-readable string to show user of your applications what the grammar looks like.
- Export to a string in the same format as the grammar file, so you can save it to a file and reuse it later.

As you may bring modifications once a grammar is parsed, the possibility to save the grammar back is a step of a complete grammar management workflow.

See the [documentation](Doc/Exportation.md) for more details about the exportation of grammars.

### Specify a custom symbol factory for parsing

All elements of the grammar use a `symbol` as unit. 

This `symbol` is an implementation of the `ISymbol` interface.
The parser creates symbols using a `symbol factory` that implements the `ISymbolFactory` interface.

By default the symbol contains only a field `name`. However, it is possible to extend the default symbol with one of your own if needed.

```csharp
class MySymbol : ISymbol
{
	public string Name { get; set; }
	public string CustomField { get; set; }
}

class MySymbolFactory : ISymbolFactory
{
	public ISymbol Create(string name)
	{
		return new MySymbol { Name = name, CustomField = "My custom data" };
	}
}

// You need to set the configurations of the parser before any parsing operation !!!
SyntaxParserConfig.GetConfig().SetSymbolFactory(new MySymbolFactory());
```

By setting the custom symbol above, all symbols will also contain a field `CustomField`.

> In practice, extending symbols is used when the symbol must also implement an external interface.

## Exceptions
This project contains several custom exceptions to handle errors that may occur during the parsing of grammar files or input strings. See a list of exceptions [here](Doc/Exceptions.md).