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

See more details in the documentation.


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

See more details in the documentation.


# Exportation to strings and files

The exportation to string has been improved, removing the extra lines added during exportation.

The exportation to file has been reviewed to export the grammar in the new syntax format.
