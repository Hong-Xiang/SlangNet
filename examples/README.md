# SlangNet Examples

This folder contains example projects demonstrating how to use SlangNet in your applications.

## HelloSlang

A simple console application showing basic SlangNet usage.

### Running the Example

```powershell
cd examples/HelloSlang
dotnet run
```

### What It Demonstrates

1. **Creating a Global Session** - Initialize Slang with default settings
2. **Custom Configuration** - Create a session with GLSL support enabled
3. **Error Handling** - Check for error messages

### Project Structure

```
HelloSlang/
├── HelloSlang.csproj    # References SlangNet via NuGet
├── nuget.config         # Configured to use local artifacts for testing
└── Program.cs           # Example code
```

### NuGet Configuration

The example uses a `nuget.config` file that points to the local `../../artifacts` folder, allowing you to test the packaged NuGet before publishing. This simulates exactly how end users will consume the package.

To use the published package instead:
1. Remove the `nuget.config` file
2. Update `HelloSlang.csproj` to use the NuGet.org source

### Code Example

```csharp
using SlangNet;

// Create a global session
var session = Slang.CreateGlobalSession();

// Create with custom settings
var desc = SlangGlobalSessionDesc.Default;
desc.EnableGLSL = true;
var sessionWithGLSL = Slang.CreateGlobalSession(desc);

// Check for errors
var errorMsg = Slang.GetLastInternalErrorMessage();
```

## Requirements

- .NET 10.0 or later
- Windows x64
- SlangNet NuGet package (2025.17.2 or later)

## Creating More Examples

To create a new example:

1. Create a new folder in `examples/`
2. Create a new console project:
   ```powershell
   dotnet new console -n YourExample -f net10.0
   ```
3. Add SlangNet package reference:
   ```powershell
   dotnet add package SlangNet
   ```
4. Copy `nuget.config` from HelloSlang for local testing
5. Add `<RuntimeIdentifier>win-x64</RuntimeIdentifier>` to the project file

## Testing Before Publishing

To test examples with the locally built package:

1. Build the package:
   ```powershell
   # From repository root
   .\build-pack.ps1
   ```

2. Run the example:
   ```powershell
   cd examples/HelloSlang
   dotnet run
   ```

The `nuget.config` automatically uses the package from `../../artifacts/`.
