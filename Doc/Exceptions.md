# Exceptions in this project
This project contains several custom exceptions to handle errors that may occur during the parsing of grammar files or input strings. Below is a list of these exceptions along with their descriptions.

# Exceptions from text parser

|**Exception**|**Description**|
|---|---|
|`EmptySymbolException`| You are trying to add or remove an empty symbol to the parser symbols list. |
|`RegisteredSymbolException`| You are trying to add a symbol that is already registered in the parser symbols list. |
|`UnregisteredSymbolException`| You are trying to remove a symbol that is not registered in the parser symbols list. |

# Grammar pre-parsing exceptions
|**Exception**|**Description**|
|---|---|
|`ImportFileNotFoundException`| The file you are trying to import was not found. |
|`InvalidEscapeCharSymbolException`| When redefining the syntax escape character, the new symbol should neither be empty nor be longer than one character|


# Exceptions from grammar parser

|**Exception**|**Description**|
|---|---|
|`EndOfStringException`| Your grammar file ends unexpectedly. Check the syntax in your grammar file. |
|`NotDeclaredTerminalException`| Use of a terminal that has not been previously declared. |
|`NotDeclaredNonTerminalException`| Use of a non-terminal that has not been previously declared. |
|`SyntaxError`| Your grammar file or string contains a syntax error. More informations are given in the error message. |
|`UnknownSymbolException`| A rule derivation contains a symbol that was not previously declared as terminal or non-terminal. |

# Internal errors
If you see one of these errors, please report it as a bug on the [GitHub repository](https://github.com/Natvs/Nt.SyntaxParser/discussions).
- `NoDefaultStateException`
- `NullRegexException`
- `NullRuleException`
