# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SysML2.NET is a .NET C# SDK implementing the OMG SysML v2 specification (based on Beta 4 pilot implementation). It provides metaclass DTOs/POCOs, serializers (JSON, XMI, MessagePack), a REST client, a DAL layer, and a Blazor WebAssembly viewer application. Current version: 0.19.0.

## Build & Test Commands

```bash
# Restore and build entire solution
dotnet restore SysML2.NET.sln
dotnet build SysML2.NET.sln

# Run all tests
dotnet test SysML2.NET.sln

# Run tests for a specific project
dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj
dotnet test SysML2.NET.Serializer.Json.Tests/SysML2.NET.Serializer.Json.Tests.csproj

# Run a single test by name
dotnet test SysML2.NET.Tests/SysML2.NET.Tests.csproj --filter "FullyQualifiedName~AcceptActionUsageExtensionsTestFixture"

# Run with coverage (as CI does)
dotnet-coverage collect "dotnet test SysML2.NET.sln --no-build" -f xml -o coverage.xml
```

Test framework: **NUnit**. Test classes use `[TestFixture]` and `[Test]` attributes.

## Architecture

### Code Generation

- favour duplicated code in codegeneration to have staticaly defined methods that provide performance over reflection based code.
- code generation is done by processing the UML model and creating handlebars templates
- **When working on the grammar/textual notation code generator** (`SysML2.NET.CodeGenerator/HandleBarHelpers/RulesHelper.cs` and related grammar processing): read `SysML2.NET.CodeGenerator/GRAMMAR.md` for the KEBNF grammar model, cursor/builder conventions, and code-gen patterns already handled.

### Code Generation Pipeline

Most code in this repo is **auto-generated** — files marked `THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!` must not be edited directly.

The pipeline:
1. **Input**: `Resources/KerML_only_xmi.uml` and `Resources/SysML_only_xmi.uml` — these two UML-based XMI files define the KerML and SysML v2 specification respectively. They are the **single source of truth** for all generated DTOs, POCOs, serializers, extension methods, and other auto-generated code. All OCL constraints (derivation rules, validation invariants, and operation body conditions) for each metaclass are also defined within these XMI files.
2. **Generator**: `SysML2.NET.CodeGenerator` reads these via `uml4net.xmi`, uses Handlebars templates (`Templates/Uml/*.hbs`) to generate code
3. **Output**: `AutoGen*` directories across multiple projects

Generator classes in `SysML2.NET.CodeGenerator/Generators/UmlHandleBarsGenerators/` produce:
- DTOs and interfaces → `SysML2.NET/Core/AutoGenDto/`
- POCOs → `SysML2.NET/Core/AutoGenPoco/`
- Enums → `SysML2.NET/Core/AutoGenEnum/`
- JSON serializers/deserializers → `SysML2.NET.Serializer.Json/Core/AutoGenSerializer/` and `AutoGenDeSerializer/`
- MessagePack formatters → `SysML2.NET.Serializer.MessagePack/`
- Extension methods (Extend) → `SysML2.NET/Extend/`
- DAL factories → `SysML2.NET.Dal/Core/`

### Project Dependency Graph

```
SysML2.NET (core: netstandard2.1)
  ├── Core/AutoGenDto/     - 342 files: DTO classes + interfaces (171 metaclasses × 2)
  ├── Core/AutoGenPoco/    - POCO classes + interfaces
  ├── Core/AutoGenEnum/    - Enums (FeatureDirectionKind, VisibilityKind, etc.)
  ├── Core/DTO/            - Hand-coded base: IElement : IData
  ├── Core/POCO/           - Hand-coded: IContainedElement, IContainedRelationship
  ├── Extend/              - Auto-generated extension methods per metaclass
  ├── Decorators/          - [Class], [Property], [Implements] attributes from UML
  ├── PIM/                 - Platform-Independent Model DTOs (REST API types)
  ├── ModelInterchange/    - Archive/project interchange types (kpar support)
  └── Common/IData.cs      - Base interface with Id property

SysML2.NET.Extensions        - Comparers, utilities across metaclasses
SysML2.NET.Serializer.Json   - JSON (de)serialization via System.Text.Json
SysML2.NET.Serializer.Xmi    - XMI (de)serialization
SysML2.NET.Serializer.MessagePack - MessagePack binary serialization
SysML2.NET.Serializer.Dictionary  - Dictionary-based serialization (PIM)
SysML2.NET.Dal               - Data Access Layer (Assembler, ElementFactory)
SysML2.NET.REST              - REST client + Session for SysML2 API servers
SysML2.NET.Kpar              - Reader/Writer for .kpar archive format
SysML2.NET.Viewer            - Blazor WebAssembly app (net9.0)
SysML2.NET.CodeGenerator     - Code generation tool (net10.0, not packaged)
```

### DTO vs POCO Pattern

Each metaclass exists in two forms:
- **DTO** (Data Transfer Object): Lightweight, uses `Guid` references for relationships. Used for serialization/transport. Properties reference other elements by `Guid` ID.
- **POCO** (Plain Old CLR Object): Rich object model with resolved object references. Used for in-memory manipulation. Uses `ContainerList<T>` for containment relationships.

Both share the same `I{MetaclassName}` interface from `AutoGenDto/`. The hand-coded `Core/DTO/IElement.cs` adds `IData` (which provides `Guid Id`) to the root interface.

### Namespace Convention

Auto-generated DTOs use structured namespaces reflecting the KerML/SysML package hierarchy:
- `SysML2.NET.Core.DTO.Root.Elements` (Element, Annotation, etc.)
- `SysML2.NET.Core.DTO.Core.Types` (Type, Feature, Classifier, etc.)
- `SysML2.NET.Core.DTO.Systems.Actions` (ActionUsage, etc.)

### Target Frameworks

- Core library (`SysML2.NET`): `netstandard2.1`
- Test projects and CodeGenerator: `net10.0`
- Viewer: `net9.0` (Blazor WebAssembly)

## Key Conventions

- Commit messages use prefix tags: `[Add]`, `[Update]`, `[Remove]`, `[Fix]`
- Main branch: `master`. Development branch: `development`
- CI: GitHub Actions (`CodeQuality.yml`) — builds, tests, and runs SonarQube analysis
- License: Apache 2.0 (code), LGPL v3.0 (metamodel files)
- To add a new metaclass: update the UML XMI source files, then run the code generators — do not manually create AutoGen files

## Quality rules

- Prefer comparing 'Count' to 0 rather than using 'Any()', both for clarity and for performance
- Use 'StringBuilder.Append(char)' instead of 'StringBuilder.Append(string)' when the input is a constant unit string
- Prefer 'string.IsNullOrWhiteSpace' over 'string.IsNullOrEmpty' when checking the non-nullable value of a string
- Prefer switch expressions/statements over if-else chains when applicable
- Prefer indexer syntax (e.g., 'list[^1]') and range syntax (e.g., 'array[1..^1]') over LINQ methods (e.g., 'list.Last()', 'list.Skip(1).Take(n)') when applicable
- Use meaningful variable names instead of single-letter names in any context (e.g., 'charIndex' instead of 'i', 'currentChar' instead of 'c', 'element' instead of 'e')
- Use 'NotSupportedException' (not 'NotImplementedException') for placeholder/stub methods that require manual implementation
