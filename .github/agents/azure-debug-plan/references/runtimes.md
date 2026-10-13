<!-- azure-cor-disclaimer -->
> **Important:** This skill provides guidance and recommended instructions to assist the AI system. Outputs are not guaranteed to be complete, correct, secure, or applicable to every scenario. Results should be reviewed and validated by a human before being applied. The AI model may choose not to follow all instructions exactly, and additional verification may be required.

# Runtimes

> **Common examples, not exhaustive.** For unlisted runtime, identify from present
> language and toolchain (e.g. `rust`, `ruby`, etc.).

## Detection Table

| Detection Signals | Runtime | Version Source |
|-------------------|---------|---------------|
| `package.json` + `tsconfig.json` | **node-ts** | `engines.node` / `.nvmrc` / `.node-version` |
| `package.json` (no `tsconfig.json`) | **node-js** | Same |
| `*.csproj` | **dotnet** | `<TargetFramework>` element (e.g. `net8.0` → `8.0`) |
| `requirements.txt`, `pyproject.toml`, or `Pipfile` | **python** | `.python-version` / `pyproject.toml` `requires-python` |
| `pom.xml` or `build.gradle` | **java** | `<java.version>` / `sourceCompatibility` |
| `go.mod` | **go** | `go` directive in `go.mod` |

---

## dotnet

### Version Detection

Read `<TargetFramework>` from `.csproj` for runtime version (e.g. `net8.0` → `8.0`).

### Assembly Name

Derive plan Service Label assembly name:

1. If `<AssemblyName>` set → use its value
2. Otherwise → `.csproj` filename without extension (e.g. `Functions.csproj` → `Functions`)
