# New syntax for writing grammars

The grammar file is now built with `blocks`, a block being a set of elements for the grammar.
A block is defined by a name and is contained between parenthesis `(` and `)`.

For example, the syntax for a terminal block is:

```
TERMINAL (
	a
	b
	c
	...
)
```

All older syntaxes are not supported anymore. Please update your grammar files to the new syntax.

See more details in the [documentation](./Doc/Grammar.md#structure-of-a-grammar-file).


# Improved the `IMPORT` statement

The pre-parsing instruction `IMPORT` has been improved. 
Now any imported file will be fully scanned before being concatenated to the current grammar file.
Thus the errors are more accurate stating the file and line in which they occur.

It still allow to reuse some of the grammar elements defined from previous imported files.

For example, if a file defines a set of terminal symbols:

`terminals.txt`
```
TERMINALS (
	a
	b
	c
)
```

then these symbols can be reused in another grammar file that imports it:

`main.txt`
```
IMPORT terminals.txt

// Now you can use the terminal symbols defined in terminals.txt
```

Please look at the [documentation](./Doc/Grammar.md#importing-an-external-grammar) for more details about the importation of grammars.

# Migration from `branching` to blocks extension

As the new syntax changes, so is the branching feature.
Now you can add custom blocks that the parser can read and interpret.

// Example of a custom block
```
CUSTOM_BLOCK (
	inside you can define any additional content you need
)
```

For more information about the blocks extension, please see the [documentation](./Doc/Extensions.md).

# Miscellaneous and bug corrections

- The previous custom data feature has been replaced by the `payload` feature. See the [documentation](./Doc/Payload.md) for more information.
- Exportation to string has been improved, removing the extra lines added during exportation.
- Exportation to file has been reviewed to export the grammar in the new syntax format.
