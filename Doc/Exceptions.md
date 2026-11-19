# Exceptions in this project
This project contains several custom exceptions to handle errors that may occur during the parsing of grammar files or input strings. Below is a list of these exceptions along with their descriptions.

## Exceptions from the parser

|**Exception**|**Description**|
|---|---|
|`UnregisteredTerminalException`| Use of a terminal that has not been previously declared. |
|`UnregisteredNonTerminalException`| Use of a non-terminal that has not been previously declared. |
|`UnknownSymbolException`| A rule derivation contains a symbol that was not previously declared as terminal or non-terminal. |
|`AxiomException`| A non-terminal was expected but the parser found either a terminal or an undefined symbol. |
|`EndOfStringException`| The grammar file ends unexpectedly. Check the syntax. |
|`ExpectedTokenException`| A token was expected but a different token was found. The exception message will provide more details. |

## Exception involving payload

|**Exception**|**Description**|
|---|---|
|`NullPayloadException`| You tried to get a payload that has not yet been set. |
|`PayloadTypeException`| The payload you tried to get exists but is of a different type than the one requested. |

## Less encountered exceptions

|**Exception**|**Description**|
|---|---|
|`ImportFileNotFoundException`| The file you are trying to import was not found. |
|`InvalidEscapeCharSymbolException`| When redefining the syntax escape character, the new symbol should neither be empty, nor `\` nor be longer than one character|

## Unlisted exceptions
Other exceptions may occur. If that happens, please [open an issue](https://github.com/Natvs/Nt.Parser/issues) on Github.

