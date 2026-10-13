# Generate Test Code

The web generator saves this checkbox in session and in metadata JSON as
`GenerateTestCode`. New projects default to checked. Imported metadata without
the setting defaults to unchecked.

- Checked: existing demo implementations, test programs, and sample object JSON
  files are generated as before.
- Unchecked: CRUD methods retain their signatures, contain
  `TODO - Add code for interacting with target system here`, and throw
  `Not Implemented`. Object JSON files and JSON storage helpers are omitted.
  Connector schema, XML, metadata, and connection setup remain; PowerShell
  logging remains available.
- Both modes include `<Project>_TEST.cs` and `<Project>_TEST.ps1`. Unchecked mode
  uses minimal examples that connect, read all objects, read one object, change
  an editable property, and update it. Classes without editable properties use
  an empty update. Examples call the real connector methods and stop at the
  first unimplemented call. They do not create or delete objects.
- Visual Studio solution output retains the library and test console project.

## Verification

Start the web app locally, then run the regression script from the repository root:

```powershell
dotnet run --project .\OIM.PS.SyncProject.Web --urls http://localhost:5187
```

In another terminal:

```powershell
.\OIM.PS.SyncProject.Web\Tests\Verify-Generation.ps1 -BaseUrl http://localhost:5187
```

The script uses its own session, generates temporary artifacts, parses and invokes
PowerShell stubs, and builds and runs the generated .NET solution. It requires
the .NET 10 SDK and PowerShell. Use a local test instance, not production.