# Enhancement of the branching feature

The `branching` feature was useful to create additional data for elements of the grammar.
However, it was sometimes unclear where to store it and hard to link it to elements.
Now the branching feature allow to store the data directly inside elements of the grammar, making it easier to manage and link the data to the corresponding elements. 
This enhancement improves the overall usability and organization of the branching feature, allowing for a more streamlined workflow when working with grammar elements.

Create data for a grammar element with:
```csharp
var data = new DataType();
<element>.CreateData(data);
```

Retrieve the stored data with:
```csharp
var data = <element>.GetData<DataType>(data);
```

Element that supports data storage:
- Terminals
- Non terminals
- Rules
- Regular expressions

More information in [the branching documentation](Doc/Branching.md).

# Trivy scanning

The workflow for building and publishing packages has been enhanced with a full Trivy scan and CodeQL reports, visible on [the Github security page](https://github.com/Natvs/Nt.SyntaxParser/security/code-scanning).

The SBOM is now generated with Trivy instead of CycloneDX, thus removing the alert due to CycloneDX using Node12.

# Other

- Automatic publication of comments on PR defining if the merge would create a new release or publish new packages.
- Possibility of publishing packages on local repository only for testing purposes with the `PackageLocalOnly` property in the .csproj file.