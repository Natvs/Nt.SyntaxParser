# Custom Blocks

- [Why Custom Blocks?](#why-custom-blocks)
- [Extending the Grammar Syntax](#extending-the-grammar-syntax)
    - [Recognition vs. Interpretation](#recognition-vs-interpretation)
- [Defining a new block](#defining-a-new-block)
    - [Creating States](#creating-states)
    - [Creating Transitions](#creating-transitions)
    - [Interpreting a new block](#interpreting-a-new-block)
- [Complete Example](#complete-example)
    - [Defining the block syntax](#defining-the-block-syntax)
    - [Implementation](#implementation)
- [Cross-applications Support](#cross-applications-support)

## Why Custom Blocks?

A grammar file describes the structure and rules of a language. 
However, depending on the application, defining a language may require more than just terminals, non-terminals, production rules, and regular expressions.
Custom blocks make the grammar format extensible, allowing different applications to share the same grammar file while including the additional information they need. 
This means that application-specific information can be kept alongside the grammar itself, without requiring modifications to parser itself or separate configuration files.

For example, an application might need to associate additional information with a grammar, such as:

- **Operator precedence and associativity:** Define how expressions involving multiple operators should be interpreted.
- **Semantic information:** Associate types, attributes, or other metadata with grammar elements.
- **Application-specific metadata:** Store information required by a particular tool or application.

Without a way to extend the grammar file format, users would have to store this information separately, modify the parser's implementation, or find workarounds using existing blocks.

## Extending the Grammar Syntax

To address this limitation, the syntax parser allows users to define their own grammar blocks.

A custom block follows the same general structure as the built-in blocks and can contain application-specific information:

```text
OPERATOR PRECEDENCE (
    + LEFT
    - LEFT
    * LEFT
    / LEFT
    ^ RIGHT
)
```

The content and structure of these blocks are entirely defined by the user. The syntax parser does not impose a predefined meaning on them.

### Recognition vs. Interpretation

It is important to distinguish between two responsibilities:

- **Recognition:** The syntax parser checks whether the content of a custom block follows the syntax defined by the user.
- **Interpretation:** The user defines what should happen when that content is encountered, using actions associated with transitions.

For example, the syntax parser can recognize an instruction such as `terminals 2 5 ;`, but it does not automatically know that this instruction should print the second through fifth terminals. The user must provide that behavior through a custom action.

## Defining a new block

After creating an instance of the syntax parser, you can register a new block using the following method:

```csharp
SyntaxBlock CreateBlock(string pattern, string expected)
```

Where:

- `pattern` is a string or regular expression that matches the opening line of your block.
- `expected` describes what the parser expects to encounter, primarily for error reporting if the expected syntax is not found.

For example:

```csharp
var parser = new SyntaxParser();
var block = parser.CreateBlock("custom|CUSTOM|Custom", "something");
```

The method returns a `SyntaxBlock` object, which contains the two states that delimit the block:

- `OpenState` is the state that represents the opening of the block.
- `CloseState` is the state that represents the closing of the block.

This registers a new block recognized by the parser, starting with either `custom`, `CUSTOM`, or `Custom`.

> **Note:** Whitespace in the pattern is used to split it into several patterns. For example, the pattern `CUSTOM BLOCK` matches the opening line `CUSTOM BLOCK`.
>
> Be careful when using alternatives. The pattern `HELLO WORLD|HI YOU` will not match `HELLO WORLD` or `HI YOU` as two independent alternatives. Instead, it matches combinations such as `HELLO WORLD YOU` or `HELLO HI YOU`.
>
> See [Microsoft's Regular Expression Language Quick Reference](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference) for information about regular expression syntax and possible workarounds.

## Interpreting Block Contents

The syntax parser does not automatically interpret the content of custom blocks. Instead, users must define their own states and transitions to describe the expected syntax.

Two concepts are essential to understanding how this works:

- **State:** Represents a position in the parsing process after reading part of the block's content.
- **Transition:** Defines when and how the parser moves from one state to another.

### Creating States

States are created through the `SyntaxBlock` instance using the following method:

```csharp
State CreateState(string expected)
```

Where:

- `expected` describes what the parser expects to encounter next. This information is used when reporting an error if the input does not match the expected syntax.

For example:

```csharp
var state = block.CreateState("a number");
```

Each state represents a position in the parsing process and describes what the parser expects to encounter next. If the input does not match the expected syntax, the parser can use this information when reporting an error.

### Creating Transitions

A transition tells the parser when to move from one state to another.

The method is:

```csharp
void SetTransition(State source, State target, string pattern)
```

Where:

- `source` is the state from which the transition starts.
- `target` is the state to which the transition goes.
- `pattern` is a string or regular expression that matches the token to read when taking the transition.

A state can have multiple outgoing transitions and can be the target of multiple incoming transitions.

However, if multiple transitions from the same state match the same input, only the first matching transition is used. Therefore, the order in which transitions are registered matters.

For example:

```csharp
var origin = block.CreateState("a number");
var target = block.CreateState("something else");

parser.SetTransition(origin, target, @"\d+");
```

This creates a transition from `origin` to `target` whenever the parser encounters a sequence of digits.

### Interpreting a new block

Once the states and transitions are defined, the syntax parser can recognize the content of the block. However, recognizing syntax does not mean interpreting it.

To define what should happen when a transition is taken, you can associate an action with it.

The method is similar to the previous one, but accepts an additional parameter:

```csharp
void SetTransition(
    State source,
    State target,
    string pattern,
    Action<Grammar, string> action
)
```

Where:

- `source` is the state from which the transition starts.
- `target` is the state to which the transition goes.
- `pattern` is a string or regular expression that matches the token to read when taking the transition.
- `action` is a function executed whenever the associated transition is taken.

The action receives two parameters:

- A `Grammar` instance, which provides access to the grammar currently being parsed.
- A `string` containing the value of the token that triggered the transition.

This allows you to process recognized tokens, update application-specific data, or perform operations on the grammar being parsed.

For example:

```csharp
var origin = block.CreateState("a number");
var target = block.CreateState("something else");

parser.SetTransition(
    origin,
    target,
    @"\d+",
    (g, t) =>
    {
        Console.WriteLine($"Read a number: {t}");
    }
);
```

Whenever a sequence of digits is encountered just after the opening of the block, the transition is taken and the associated action is executed.

## Complete Example

Let's say we want to extend our grammar file format with a `PRINT` block.

This block will allow us to print selected elements of the grammar:

```text
TERMINALS (
    a
    b
    c
    d
    e
)

NON TERMINALS (
    A
    B
    C
    D
    E
)

PRINT (
    terminals 2 5 ;
    non-terminals 1 3 ;
)
```

Whenever a `PRINT` block is encountered, we want to print the requested elements.

For example, `terminals 2 5 ;` means that we want to print the second through fifth terminals.

Although the syntax parser already knows how to interpret the `TERMINALS` and `NON TERMINALS` blocks, `PRINT` is a custom block. Therefore, we must define both its syntax and its behavior.

### Defining the block syntax

Each instruction inside the `PRINT` block follows this structure:

```text
terminals <start index> <end index> ;
```

or:

```text
non-terminals <start index> <end index> ;
```

To recognize these instructions, we need four states:

1. `block.OpenState`: Reads the type of element to print.
2. `StartState`: Reads the starting index.
3. `EndState`: Reads the ending index.
4. `PrintState`: Reads the semicolon and executes the printing action.
5. `block.CloseState`: Returns to the beginning of the next instruction or end the block.

After an instruction has been processed, the parser returns to the starting state of the instruction so that another one can be read.

### Implementation

```csharp
using Nt.Syntax;
using Nt.Syntax.Blocks;

var parser = new SyntaxParser();

// Register the custom block.
var block = parser.CreateBlock(
    "PRINT",
    "'terminals' or 'non-terminals'"
);

// Create the states.
var startState = block.CreateState("a starting index");
var endState = block.CreateState("an ending index");
var printState = block.CreateState("a semicolon");

// Variables used to store the instruction being processed.
var typeToPrint = "";
var startIndex = 0;
var endIndex = 0;

// Read the type of elements to print.
parser.SetTransition(
    block.OpenState,
    startState,
    "terminals|non-terminals",
    (g, t) => typeToPrint = t
);

// Read the starting index.
parser.SetTransition(
    startState,
    endState,
    @"\d+",
    (g, t) => startIndex = int.Parse(t)
);

// Read the ending index.
parser.SetTransition(
    endState,
    printState,
    @"\d+",
    (g, t) => endIndex = int.Parse(t)
);

// Print the requested elements when the semicolon is encountered.
parser.SetTransition(
    printState,
    block.CloseState,
    ";",
    (g, t) =>
    {
        if (typeToPrint == "terminals")
        {
            for (var i = startIndex; i <= endIndex; i++)
            {
                Console.WriteLine(g.Terminals[i - 1]);
            }
        }
        else if (typeToPrint == "non-terminals")
        {
            for (var i = startIndex; i <= endIndex; i++)
            {
                Console.WriteLine(g.NonTerminals[i - 1]);
            }
        }
    }
);

// Return to the beginning of the next instruction.
parser.SetTransition(
    block.CloseState,
    startState,
    "terminals|non-terminals",
    (g, t) => typeToPrint = t
);

// Parse the grammar file.
parser.ParseFile("path/to/the/file.txt");
```

In this example, the actions associated with the transitions store the information needed to process each instruction.

When the parser encounters a semicolon, the corresponding action retrieves the requested elements from the grammar and prints them.

The transition from `block.CloseState` back to `startState` allows multiple instructions to be processed within the same `PRINT` block.

**Note:** The indices in this example are one-based, meaning that the first element has index `1`. The code subtracts `1` when accessing the underlying C# collections, which are zero-based.

This example demonstrates how custom blocks can extend the grammar file format with application-specific syntax and behavior, while leaving the built-in parsing functionality unchanged.

---

## Cross-applications Support

The ability to extend the grammar syntax allow multiple applications to share the same grammar file while including their own specific information. 
Each application can define its own custom blocks, states, and transitions without interfering with others.

However, all applications may not define all blocks, so to ensure there are no errors, just add a directive in your file.

```
ignore_unknown

UNKNOWN BLOCK (
    if this block is not defined in the application, it will be ignored
)
```

> **Note:** The directive can be written in many different ways, such as `ignore_unknown`, `IgnoreUnknown`, or `IGNORE_UNKNOWN`.
> It should match `([Ii]gnore|IGNORE)_?([Uu]nknown|UNKNOWN)_?([Bb]locks|BLOCKS)?`