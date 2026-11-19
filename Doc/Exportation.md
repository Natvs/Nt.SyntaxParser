# Exportation

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Exportation methods](#exportation-methods)
- [Exportation modes](#exportation-modes)
  - [ExportationMode.Original](#exportationmodeoriginal)
  - [ExportationMode.Alphabetical](#exportationmodealphabetical)
  - [ExportationMode.Grouped](#exportationmodegrouped)
  - [Common behaviors of the exportation modes](#common-behaviors-of-the-exportation-modes)
	- [Rules reordering](#rules-reordering)
	- [Rules grouping](#rules-grouping)
- [Typical workflow](#typical-workflow)

Exporting a grammar means getting a reusable or informational object (string, grammar code, new grammar file) out of a grammar.

## Introduction

The parser is used to create a grammar structure from a grammar file. 
At some point, the grammar may either be prompted or saved to a file.
The `Exportation` feature brings methods to the grammar to answer those needs.

Currently, the following exportation methods are provided:
- Print the grammar to a user-readable string
- Write the grammar to a string with the grammar file syntax

## Prerequisites

The exportation methods are not enabled by default. You need to enable these to use this feature.

```csharp
import Nt.Syntax.Exportation;
```

## Exportation methods

The exportation methods are:


|Method|Parameters|Return type|Comment|
|------|----------|-----------|-------|
|`ToString(mode)`|`mode` is one value of `ExportationMode`|string|Use this method to export a grammar as a user-readable grammar.|
|`ToString()`||string|Same as above, but `mode` is always `ExportationMode.Original`. This method is available without importing `Nt.Syntax.Exportation`|
|`Save(mode)`|`mode` is one value of `ExportationMode`|string|Use this method to export a grammar in a grammar file syntax. The grammar can then be loaded again later.|

# `ToString`

The method `ToString` is used to export a grammar as a user-readable grammar for display purpose.
This is NOT the same syntax as a grammar file, but a more user-friendly syntax.

Example:
```
Terminals:
- a
- b
- ...

Non terminals:
- A
- B
- ...

Rules:
- A -> a B
- B -> b
- ...

Regular expressions:
- Number = [0-9]+

```

## Exportation modes

The value of the `mode` parameter are:

|Name|Behavior|
|----|--------|
|ExportationMode.Original|Order of symbols and rules in the output grammar is the same as the one they are declared|
|ExportationMode.Alphabetical|Symbols and rules are reordered in the alphabetical order|
|ExportationMode.Grouped|Symbols with a common root in the name are grouped together. This is interesting once the grammar receives rules treatment and some symbols (terminals or non terminals) are extended from other.|

### `ExportationMode.Original`

As the name suggests, the `ExportationMode.Original` mode will export the grammar as it was declared in the grammar file.
No symbols or rules will be reordered.

### `ExportationMode.Alphabetical`

The `ExportationMode.Alphabetical` mode will export the grammar with symbols and rules ordered alphabetically.

### `ExportationMode.Grouped`

The `ExportationMode.Grouped` rules groups the different symbols and rules beginning with a specific axiom without changing the overall order of the rules.

For example, if the grammar has the following rules:

```
B -> hello
A -> this should be the last rule
B -> world
```

the `ExportationMode.Grouped` will regroupd the rules as following:

```
B -> hello | world
A -> this should be the last rule
```

### Common behaviors of the exportation modes

#### Rules reordering

Reordering rules or regular expressions is slightly different from reordering symbols.

Indeed, symbols are composed of only one component: the name of the symbol itself.
However, rules and regular expressions are composed of two components: the axiom and the production.
Thus, the rules and regular expressions are grouped by axiom first, then by their production.

For example, with the `ExportationMode.Alphabetical` mode:

```
B -> hello you | hello me
```

the two rules have the same axiom, but the first difference is the second component of the production.
Thus, as `me` comes before `you`, the rules will be reordered as:

```
B -> hello me | hello you
```

#### Rules grouping

All exportation methods will also group consecutive rules with the same axiom with the syntax:

```
A -> hello
A -> world
```

becomes

```
A -> hello | world ;
```

## Typical workflow
Exportation methods are meant to be used in workflows where
1. A grammar is created from scratch or loaded from an existing grammar file
2. The grammar goes through various modifications, like grammar pre-treatment for further analysis
3. An exportation method is used to store the grammar or display it to a user

In your code, it will look something like this:

```csharp
using Nt.Syntax;
using Nt.Syntax.Exportation;

var parser = new SyntaxParser();
var grammar = parser.ParseFile("path/to/a/grammar/file");

--- Various modifications ---


-----------------------------

File.WriteAllText("path/to/the/output/file", grammar.Save(ExportationMode.Original));
```