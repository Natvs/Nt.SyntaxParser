# SyntaxParser

- [Introduction](#introduction)
- [Advantages of using Nt.SyntaxParser](#advantages-of-using-ntsyntaxparser)
- [Prerequisites](#prerequisites)
	- [Install the Nt.SyntaxParser package](#install-the-ntsyntaxparser-package)
	- [Define the grammar syntax](#define-the-grammar-syntax)
- [Using the syntax parser](#using-the-syntax-parser)
- [Additional features of the parser](#additional-features-of-the-parser)
	- [Add new blocks to the grammar file](#add-new-blocks-to-the-grammar-file)
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
- Store custom payload into an element of the grammar
- Add custom configurations within the same file that describes the grammar
- Supports importation for splitting the grammar into multiple ones
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



> The unit of the parser is the `symbol`. The lists of terminals and non-terminals, or even the derivation rules are all made of symbols.
> These symbols are by default an instance of the `Symbol` class and only contain a name. For advanced use cases, you may need to create your own symbol class.
> In this case, the custom symbol must extend the `ISymbol` interface and you have to provide a custom symbol factory implementing the `ISymbolFactory`.
> You can then set this factory as default factory for parsing with `SyntaxParserConfig.GetInstance().SetSymbolFactory(new_factory)`.

## Additional features of the parser

### Add new blocks to the grammar file

Whether you want to add more information to the grammar or add custom configurations, you can add new blocks to the grammar file on top of the 5 existing ones.

### Store custom payload into an element of the grammar

you need to add external information to an element of the grammar, but don't know how to store it ?
The `payload` feature is meant to link elements like terminals or rules with additional information, like tags, conditions or any other information.

With the `payload` feature activated, there is no need to manage external data structures as the data is contained inside the grammar itself.
The payload accepts any structure of data, so it's up to you which data you want to store.

The whole feature, and how to activate it, is described in the [payload documentation](Doc/Payload.md).

### Export the grammar to a string

Once you created a grammar, you can choose to export it to a string.
The exported string can be in different formats, depending on your needs.

- Export to a user-readable string to show user of your applications what the grammar looks like.
- Export to a string in the same format as the grammar file, so you can save it to a file and reuse it later.

As you may bring modifications once grammar is parsed, the possibility to save the grammar back is a step of a complete grammar management workflow.

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