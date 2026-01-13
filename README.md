# SyntaxParser

## Introduction
This project takes a grammar file as input and parses into a grammar structure.
It aims to be used when creating new software languages.

## Grammar structure
The grammar structure contains the following elements:
- A set of terminal symbols (constant strings)
- A set of non-terminal symbols (symbols to derive)
- A set of derivation rules (rules to derive non-terminals into sequences of terminals and non-terminals)
- A set of regular expression rules (rules to derive non-terminals into regular expressions)
- An axiom (the starting non-terminal symbol for derivations)

Inside the grammar, rules and regular expressions are stored in a set and thus order does not matter.

> A rule is composed of a symbol (or token) and a derivation. The derivation is a sequence of terminals and non-terminals that the symbol can be derived into.

## Grammar file
The syntax parser uses a grammar file to create a grammar structure.

A grammar file is a text file that describes the grammar of a language. It contains the list of symbols used in the grammar and the rules. The syntax of the grammar file is described in the [grammar file syntax documentation](Doc/Grammar.md).

## Using the syntax parser
To use the syntax parser, you first need to create an instance of the `GrammarParser` class. There are two ways of parsing a grammar file: from a string or from a file.

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

Also parsing a grammar always applies the pre-parser on it, it's also possible to get a string representing the grammar after pre-parsing with the method `PreParseString(string grammarString)`.

## Using the text parser
You can also use the customisable text parser included in this project to parse strings.

The customisation parameters are:
- a list of separators: these separators are used to split the input string into tokens (default is `[' ']`)
- a list of symbols: these symbols are used to identify tokens in the input string (default is `[]`)

You can create an instance of the `TextParser`
```csharp
using Nt.Parsing;
var parser = new Parser(separators, symbols);
```

Use the method `SetSymbols`, `AddSymbol` or `RemoveSymbol` to set the symbols after creating the parser.

To parse a string, use the method `ParseString`
```csharp
ParserResult parsed = parser.ParseString(inputString);
```

A `ParserResult` is a structure that contains
- a list of tokens: list of the unique symbols that are present in the input string
- a list of token indices: list of parsed tokens indices, each index corresponds to a token in the tokens list

### Escape character
The escape character '\\' is a special character that allows any symbol following it to be parsed in the continuity of the token being currently parsed instead of creating a new one. The escape character is used when you need to include symbols or separators in a token.

For example, consider the two following cases:
1. Consider a parser with `+` defined as a symbol. Then "a+b" will be parsed as 3 tokens : 'a', '+' and 'b'. With the escape character, "a\\+b" is parsed as a unique symbol "a+b".
2. Consider a parser with "\_" defined as a separator. Then "a_b" will be parsed as 3 tokens: 'a', '\_' and 'b'. But "a\\\_b" is parsed as the unique symbol "a\_b".

# Exceptions
This project contains several custom exceptions to handle errors that may occur during the parsing of grammar files or input strings. See a list of exceptions [here](Doc/Exceptions.md).