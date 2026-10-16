# Branching

The update 1.3.0 introduces a new `branching` feature. This new feature allows to add and interpret custom syntax in the grammar file and enhanced grammar.

This feature is useful if:
- You want to add additional parameters in the grammar file destined to your program
- You want to specify additional parameters to terminals, non terminals, rules or regex
- You want to write your own modules with your own syntax for your project in the grammar file
- Other scenarios where you want to extend the grammar file with your own syntax

To view the full documentation of the `branching` feature, please refer to the [documentation](Doc/branching.md).

# Fix

Although the documentation precised how to write the axiom of a grammar file with the new syntax, the syntax was not yet implemented. This updates fixes it.