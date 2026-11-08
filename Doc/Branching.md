# Branching

- [When to use branching](#when-to-use-branching)
- [Example of a use case](#example-of-a-use-case)
- [Create a new branch](#create-a-new-branch)
  - [1. Import the necessary namespaces](#1-import-the-necessary-namespaces)
  - [2. Define the states and transitions](#2-define-the-states-and-transitions)
  - [3. Link the first and final states to the parser](#3-link-the-first-and-final-states-to-the-parser)
  - [4. Add actions to the states and transitions for the parser to interpret the new syntax](#4-add-actions-to-the-states-and-transitions-for-the-parser-to-interpret-the-new-syntax)
- [Use the automaton context](#use-the-automaton-context)

## When to use branching

A grammar file must contain all data to construct a grammar as described in the [Grammar](Grammar.md) section.
For some scenarios, programs may be provided more information about aspects of the grammar, like abstraction rules or other custom values.
Without `branching`, this would require creating an other file referencing some elements of the grammar.
But the referencement can be hard to describe, and bound to errors whenever a file is edited.  `Branching` provides a clearer and more compact solution to that problem.

So `branching` is used when:
- You want to associate a value or some additional information to a terminal, non-terminal, rule or regular expression
- You need to define some additional parameters for your program that the syntax parser don't already handle
- You want to make it more than just a grammar file, but also a configuration file for your program
- Other scenarios where you want to define some custom syntax in the grammar file, and have it parsed by the parser.

## Example of a use case
For example, you can define a rule, and then define some value associated to that rule without having to define it in a new file.

```csharp
N = {A}
T = {this, is, my, rule}

R: A --> this is my rule; 
VAL = 1;
```

In those lines, the line `R: A --> this is my rule;` is a known command from the grammar parser. 
However, the following line `VAL = 1;` used to define a value associated to the rule 'A' is not known and would raise an error.
`branching` offers the possibility to read this new syntax and associate the value to the rule.

## Create a new branch

To allow the parser to read the new syntax, you need to define a branching mechanism that can recognize and process the custom syntax.
This works with defining `states` and `transitions` between those states, where the transitions define how to switch between states based on the token being parsed.

In the following, we will add the line `VAL = 1;` to the grammar parser.

This is done in 4 steps:
1. Import the necessary namespaces
2. Define the branching states and transitions, representing the syntax to be parsed
3. Link the branch to the actual parser
4. Tell the parser how to interpret the new syntax

### 1. Import the necessary namespaces
```csharp
using Nt.Syntax;
using Nt.Syntax.Branching;
using Nt.Automaton.States.State;
using Nt.Automaton.Transitions;
```

### 2. Define the states and transitions

Define the states and transitions that will represent the syntax to be parsed.

> When defining the transitions, don't mind the first and last transitions (here `VAL` and `;`) as they will be declared in a different way later.

```csharp
// Define the states needed
var keywordState = new State<string>();
var equalState = new State<string>();
var valueState = new State<string>();

// Define the transitions between the states
equalTransition = new Transition("=", equalState);
valueTransition = new Transition(valueState);

keywordState.AddTransition(equalTransition);
equalState.SetDefault(valueTransition);
```

### 3. Link the first and final states to the parser

The automaton is still local and not linked to the parser. To link it, we need to tell the parser when to enter the branch and when to exit it.

```csharp
var parser = new SyntaxParser();

// Go to the first state when reading the token `VAL`
parser.StartBranch("VAL", keywordState);

// Return to the main parser when reading the token `;`
parser.EndBranch(valueState, ";")
```

### 4. Add actions to the states and transitions

Now that the syntax can be recognized by the parser, we need to tell the parser how to interpret it. This is done by adding actions to the states and transitions.

```csharp
// Add an action to a transition
valueTransition.AddAction(
	(context, token) => 
	{
		Console.WriteLine($"The value for the rule '{context.Rule}' is: {token.Value}")
	}
)

// Or add an action to a state
valueState.AddAction(
	(context) => {
		Console.WriteLine($"The value of `{context.Rule}` has been read.")
	}
)
```

## Use the automaton context

The two methods `AddAction` both have a first argument `context`. This context provides information about the current state of the parsing processus, that can be used to retrieve information.

| Method or field | Description |
|--------|-------------|
| GetPath() | Return all the imported paths declared by the `ADDTOPATH` statement in the grammar. |
| ImportedString | Represent the last string that had been imported by an `IMPORT` statement. |
| Terminal | Represent the last terminal parsed. |
| NonTerminal | Represent the last non-terminal parsed. |
| Rule | Represent the last rule that had been parsed. |
| Regex | Represent the last regular expression that had been parsed. |

## Custom data storage

One main aspect of the branching feature is about defining custom data for elements like terminals, non-terminals, rules or regular expressions.
This new data has to be stored somewhere. There are two ways of storing this data

- Either externally in a structure you define and with a link to an element of the grammar. This requires you to handle everything, from storage to linking.
- Either internally in the element of the grammar itself, in a predefined structure for that effect. This method allows you to avoid complexity of storage and linking. That is what this section is about.

To store internally, follow the following steps:

### 1. Define the structure of the data

The data is a class that implements the interface `IData`.

```csharp
public class MyData : IData
{
	// Define your custom data here
}
```

### 2. Store the data in the element of the grammar

Use the action of a transition or a state to store the data the parser reads from your custom syntax.

```csharp
// This action edits the data of the last terminal read by the parser
valueTransition.AddAction(
	(context, token) => 
	{
		var data = new MyData();
		data.MyValue = token.Value;
		context.Terminal.CreateData(data);
	}
)

// This action edits the data of the last rule read by the parser
valueState.AddAction(
	(context) => {
		var data = new MyData();
		data.MyBool = true;
		context.Rule.CreateData(data);
	}
)
```

### Access the data later

To access the data later, you can use the `GetData<T>` method on the element that has the data.

Here are some examples of accessing data:

```csharp

var parser = new SyntaxParser();
var grammar = parser.ParseFile("path/to/a/grammar/file.txt");

foreach (var rule in grammar.Rules)
{
	// Access the data of the rule
	var data = rule.GetData<MyData>();
	Console.WriteLine($"The value associated to the rule is: {data.MyBool}");
}

// Access the data of the last rule read by the parser
var ruleData = context.Rule.GetData<MyData>();
Console.WriteLine($"The value of the rule is: {ruleData.MyBool}");
```

Be aware that if the data is not initialized, the method `GetData<T>` will throw an exception.

