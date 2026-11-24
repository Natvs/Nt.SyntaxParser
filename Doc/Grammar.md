# Grammar file syntax

- [Introduction](#introduction)
- [Structure of a grammar file](#structure-of-a-grammar-file)
  - [Terminals](#terminals)
  - [Non-terminals](#non-terminals)
  - [Rules](#rules)
  - [Regular expressions](#regular-expressions)
- [Importing an external grammar](#importing-an-external-grammar)
  - [Import from the grammar file](#import-from-the-grammar-file)
  - [Use the imported grammar](#use-the-imported-grammar)

## Introduction

A grammar is a structure used to define a language.
It consists of a set of symbols, a set of rules and an axiom.

There are two types of symbols:
- `Terminals` are symbols that are constant strings. They are the building blocks of the language and can be keywords, operators, or other fixed elements.
- `Non-terminals` are symbols used in rules and regular expressions. The rules define how those non-terminals can be derived into sequences of terminals and non-terminals.

The `axiom` is the first non-terminal of the grammar. It means that all languages described by the grammar are derivations of the axiom.

A particularity of this syntax parser is the possibility to define regular expressions along with the other elements of the grammar, avoiding the troubles of manipulating many rules for creating some.

## Structure of a grammar file

All of these elements are defined in a grammar file. 
Each type of element described in a `block`. 
A block begins with the name of the element, and the description is between parentheses.

As there are five elements in the grammar, there are also five blocks in a grammar file.

> A block that is repeated multiple times in the file won't overwrite itself but instead each redefinition will extend the initial block.

### Terminals

Terminals are defined in a terminal block.
All unique terminals must be separated by (at least) one space or new line.

```
TERMINALS (
	if
	else
	while
	for
	return
	+
	-
	*
	/
)
```

You may want to include the symbol `)`. In this case, you should escape it `')`.
The escape character is configurable. Add `ESCAPE` followed by a character in the file to change it.

The following example shows how to escape the terminal `)` with the character `#`.
```
ESCAPE #

TERMINALS (
	#)
)
```

The escape character must be only one character, and it must not be `\`.

### Non-terminals

Non-terminals are defined in a non-terminal block.
All unique non-terminals must be separated by (at least) one space or new line.

```
NON TERMINALS (
	Expression
	Statement
	Function
)
```

### Rules

Rules define how non-terminals can be derived into sequences of terminals and non-terminals.
There are defined in a rules block. 
Each rule begins with the non-terminal it defines, called `axiom` of the rule.
The axiom and the production are separated by an assignation `->`, `=>` or `:=`.
The production must be ended by a semicolon `;`.

```
RULES (
	A -> a B c ;
	B -> b B ;
)
```

### Regular expressions

Regular expressions are similar to rules in that they also define how a non-terminal can be derived into a production, though the production is a string.
The axiom and the production are separated by an assigination `=` or `:=`.

```
REGULAR EXPRESSIONS (
	Identifier = [a-zA-Z_][a-zA-Z0-9_]*
	Integer = [0-9]+
)
```

Regular expressions can only be defined one time for a same axiom. 
A good practice also states that there can't be non terminals being axioms of both a rule and a regular expression.


## Importing an external grammar

### Import from the grammar file

The parser supports importing external grammars into the current one. 
The file to import is described in the grammar file with an `IMPORT` statement.

```
IMPORT path/to/grammar/file.txt
```

By default, the parser will look for a file of exactly the name after the `IMPORT` from the same directory as the current grammar file.
It is however possible to specify a path to look for the file before importing it.

```
ADDTOPATH path/to/look/for/grammars
IMPORT grammar.txt
```

The parser will concatenate all files with the provided paths in the order they are defined.
If no path contains such a file, the parser will look in the current directory.
If there are still no file, the parser throws a `ImportFileNotFoundException` exception.

```
ADDTOPATH my/first/path
ADDTOPATH my/second/path
IMPORT grammar.txt
```

The parser will first look for `my/first/path/grammar.txt`, then `my/second/path/grammar.txt`, and finally `./grammar.txt`.
Only the first one will be imported. If none of those files exist, the parser will throw an exception.

### Use the imported grammar

After importing a grammar, all elements of the imported grammar are available in the current grammar.

Here is an example:

`imported_grammar.txt`
```
NON TERMINALS (
	IDENTIFIER
)
```

`grammar.txt`
```
IMPORT imported_grammar.txt

REGULAR EXPRESSIONS (
	IDENTIFIER = [a-zA-Z_][a-zA-Z0-9_]*
)
```

In this example, the non-terminal `IDENTIFIER` is defined in an external grammar file.
The importation allows the current grammar to use it in a regular expression.