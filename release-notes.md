# Save grammar to files
This update introduces a new method for exporting grammars.

While the previous one is about exporting it to readable texts by the users, this one is about exporting it to files syntax so that it can be later parsed back to a grammar.

For that, import the `Nt.Syntax.Exportation` namespace and then use the `Save` extended method on the grammar object.

```csharp
using Nt.Syntax.Exportation;

var grammar_string = grammar.Save(ExportationMode.Initial);
```

# Exportation enhancement
Now when exporting a grammar, the subsequent rules are grouped with ` | ` for better readability.

As a consequence, when exporting with the `ExportationMode.Grouped` or `ExportationMode.Alphabetical` modes, all the rules with a common symbol will be grouped together.

# Security integration
The project integrates code security check at build time using the default Dotnet tools.