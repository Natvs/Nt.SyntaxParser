# Grammar file syntax

Parsing the grammar file occurs in two steps. The grammar file is first parsed to process pre-parsing lines.
The resulting file is then parsed into the final grammar file that would be parsed into a grammar structure.

## Defining symbols used in the grammar
You need to provide the grammar with a list of the symbol used before using them.
These symbols are divided into terminals (constant strings) and non terminals (symbols to derive).

Terminals and non terminals are both listed into a list of the form `{symbol1, symbol2, ...}`. What changes however is the start of the line.

To define terminals, use:
```
T = {terminal1, terminal2, ...}
```
To define non terminals, use:
```
N = {nonterminal1, nonterminal2, ...}
```

Note that symbols defined here can contain any characthers except the symbols `,` `}` `\` or any white space. If you wish to add a symbol containing such a character into your grammar, you must use the escape character `\`.  See [the parser escape characters](../README.md#escape-character) for more details.

## Setting grammar rules
You need to provide each non-terminal at least one derivation rule. The derivation can be any sequence of previously defined symbols.

These rules have the following syntax:
```
R: non-terminal -> symbol1 symbol2 symbol3 ... ;
```

You can define multiple derivations for a same non-terminal with the syntax:
```
R: non-terminal -> symbol11 symbol12 ... | symbol21 symbol22 ... ;
```

You need to separate all your symbols by at least one white space. White spaces you write are ignored when parsing. If you wish to include a white space as a symbol, or a symbol among `,` `}` `\`, you can use the escape character `\`. See [the parser escape characters](../README.md#escape-character) for more details.

A derivation should be followed with `;` to mark the end of the rule. Remark that the escape symbol `\;` is interpreted by the parser as `;` and thus also marks the end of the rule. Please have a look at [the section about escape characters](#escape-characters) for a way to include it.

> Note that the arrow `->` can be as long as you want. It must begin with `-` and ends with a `>`. So `---->` is also valid.

## Defining regular expressions
Alternatively, you define a derivation for non-terminal as a regular expression.
Regular expressions can always be described by a grammar. However it is sometimes a hassle to set terminals, non-terminals and many rules that can make your code less manageable.
You can use regular expressions without having to previously define the symbols used in it and define it all in just one rule. The syntax is a follow:

```
E: non-terminal = regex ;
```

Unlike rules, the escape characters does not have the same meaning. See [official microsoft documentation](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference) to learn more about regex.

## Setting grammar first symbol
The grammar needs a symbol to begin with for syntaxically defining if a string belongs to the grammar language. This symbol is called axiom and is defined with the syntax:
```
S = axiom
```
The axiom can be any non-terminal, but only one of those.

## Escape characters
Here there is a need to distinguish the [escape character for text parsing](../README.md#escape-character) and the one for syntax parsing.
- For **text parsing**, the escape character is `\` and only ensures the next character is in the continuity of the token.
- For **syntax parsing**, the escape character is `'` by default and is used in rules definitions to ensure the derivation symbol is treated as as part of a terminal or non terminal.

When writing a grammar, here is how to use each escape character:

1. Use `\` for symbols `:`, `,`, `=`, `{`, `}`, `;`, `-`, `>`, `+`, `*` and `\` to include them in the continuity of the token. If not present, it will create a new token. Note that this escape character also works for any symbol.

2. Use `'` in rule derivations definitions for symbols `|` and `'` to define them as rule symbols and not rule end markers. The escape character `'` can also be used for any character, though it has no effect.

3. To include a terminal containing `,` or `}` in a terminals set (`T={...}`), use the combination `'\,` and `'\}`

4. To include `;` in a rule derivation or a regular expression, use the combination `'\;`.



## Pre-parsing instructions
Before parsing the grammar, a pre-parser is first applied to the grammar file. It consists of pre-parsing instructions which stands for configuration commands. Both the upper and lower case versions of these instructions are accepted.

| Instruction | Meaning | Example |
| --- | --- | --- |
|addtopath|Adds a folder to the import path|ADDTOPATH ../grammars|
|import|Imports the content of an external grammar file|IMPORT grammar.txt|
|escape|Redefines the syntax escape character (`'` by default)|ESCAPE $|

For example, if you have a grammar file under "../grammars/grammar.txt", then there are two ways of importing it.
```
IMPORT ../grammars/grammar.txt
```
or
```
ADDTOPATH ../grammars
IMPORT grammar.txt
```

## Example of grammar file use case
Consider the following grammar files.

File `const.txt`
```
N = {CONST, STRING, INT, FLOAT}

R: CONST -> STRING | INT | FLOAT ;

E: STRING = [a-zA-Z]* ;
E: INT = (0|[0-9]*) ;
E: FLOAT = [0-9]+(,[0-9]+)? ;
```

File `params.txt`
```
N = {PARAMS, TYPE, STRING}
T = {;, '\,}

R: PARAMS -> TYPE STRING '\;
		   | TYPE STRING , PARAMS '\; ;

E: TYPE = (int|double|char|string) ;
```

These files can't be directly used as grammars as they don't define any axiom. In params, the symbol `STRING` that is used is not even defined.
Where these files could be of interest, is when imported into other big files.

```body.txt
IMPORT const.txt
IMPORT params.txt

T = {(, ), {, }, ;, =}
N = {BODY, TYPE, STRING, CONST}
S = BODY
R: BODY -> TYPE STRING '\;
		 | TYPE STRING = CONST '\;
		 | TYPE STRING ( ) { BODY } '\;
		 | TYPE STRING ( PARAMS ) { BODY } '\; ;
```

After pre-parsing, the grammar becomes something like
```
T = {;, ,, (, ), {, }, =}
N = {CONST, STRING, INT, FLOAT, PARAMS, TYPE, BODY}

S = BODY

R: CONST -> STRING | INT | FLOAT ;
R: PARAMS -> TYPE STRING '\;
		   | TYPE STRING , PARAMS '\; ;
R: BODY -> TYPE STRING '\;
		 | TYPE STRING = CONST '\;
		 | TYPE STRING ( ) { BODY } '\;
		 | TYPE STRING ( PARAMS ) { BODY } '\; ;

E: STRING = [a-zA-Z]* ;
E: INT = (0|[0-9]*) ;
E: FLOAT = [0-9]+(,[0-9]+)? ;
E: TYPE = (int|double|char|string) ;
```

Note that pre-parsing instructions does not works recursively, meaning you can import files only from you main grammar file.
