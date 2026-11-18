# Data payload

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Creating and accessing data payload](#creating-and-accessing-data-payload)
  - [1. Define the payload structure](#1-define-the-payload-structure)
  - [2. Store a new payload in an element of the grammar](#2-store-a-new-payload-in-an-element-of-the-grammar)
  - [3. Access the payload](#3-access-the-payload)

## Introduction

This project parses a grammar file and produces elements such as terminals, non-terminals, rules and regular expressions.
These created elements have a set number of properties defined by the parser.

You may want in some scenarios to add extra data to these elements, such an new fields or properties.
To that purpose, `data payload` is a feature that allows you to store arbitrary types of data structure in the elements of the grammar.

The following elements support the data payload feature:
- Terminals
- Non-terminals
- Rules
- Regular expressions

## Prerequisites

The payload structure is a feature not enabled by default. You need to enable it to use this feature.

```csharp
import Nt.Syntax.Payload;
```

## Creating and accessing data payload

### 1. Define the payload structure

The payload is a class that implements the interface `IPayload`.

```csharp
public class MyData : IPayload
{
	// Define your custom data here

	int MyValue { get; set; }
}
```

### 2. Store a new payload in an element of the grammar

Use the method `SetPayload` on an element of the grammar to attach a new payload.

```csharp
<element>.SetPayload(new MyData() { MyValue = 42 });
```

### 3. Access the payload

To access the data later, you can use the `GetData<T>` method or the `TryGetData<T>` method on the element of the grammar.

There is a key difference between those two methods:
- `GetPayload<T>` returns the payload if the stored payload is set and is of the requested type. It will throw an exception otherwise.
- `TryGetPayload<T>` returns a boolean indicating if the data was found or not and is of the requested type.

```csharp
try 
{
	var payload = <element>.GetPayload<MyData>();
}
catch (NullPayloadException ex)
{
	// Handle the case where the payload is not initialized
}
catch (PayloadTypeException ex)
{
	// Handle the case where the payload is of a different type
}
```

or 

```csharp
if (<element>.TryGetPayload<MyData>(out var payload))
{
	// Use the payload
}
else
{
	// Handle the case where the payload is not found or is of a different type
}
```