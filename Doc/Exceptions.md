# Exceptions in this project
This project contains several custom exceptions to handle errors that may occur during the parsing of grammar files or input strings. Below is a list of these exceptions along with their descriptions.

## Exceptions from text parser
See [the parser projet official documentation](https://github.com/Natvs/Nt.Parser) on Github for a list of exceptions from the text parser.

## Grammar pre-parsing exceptions
|**Exception**|**Description**|
|---|---|
|`ImportFileNotFoundException`| The file you are trying to import was not found. |
|`InvalidEscapeCharSymbolException`| When redefining the syntax escape character, the new symbol should neither be empty nor be longer than one character|


## Exceptions from grammar parser

|**Exception**|**Description**|
|---|---|
|`EndOfStringException`| The grammar file ends unexpectedly. Check the syntax. |
|`UnregisteredTerminalException`| Use of a terminal that has not been previously declared. |
|`UnregisteredNonTerminalException`| Use of a non-terminal that has not been previously declared. |
|`SyntaxError`| Your grammar file or string contains a syntax error. More informations are given in the error message. |
|`UnknownSymbolException`| A rule derivation contains a symbol that was not previously declared as terminal or non-terminal. |

## Unlisted exceptions
Other exceptions may occur. If that happens, please [open an issue](https://github.com/Natvs/Nt.Parser/issues) on Github.
