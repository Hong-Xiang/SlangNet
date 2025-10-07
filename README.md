# SlangNet

A .NET binding for the [Slang shader compiler](https://shader-slang.com/). This library provides modern COM interop with source generators and high-level C# APIs to interact with the Slang native library.

## Features

- ✅ Modern COM interop using .NET source generators
- ✅ Native AOT compatible
- ✅ HRESULT-based error handling
- ✅ Automatic reference counting via ComWrappers
- ✅ P/Invoke bindings using LibraryImport
- ✅ NuGet package with all native dependencies included
- ✅ Support for Windows x64 (win-x64)

## Installation

```bash
dotnet add package SlangNet
```

## Quick Start

### Creating a Global Session

```csharp
using SlangNet;

// Create a global session with default settings
var session = Slang.CreateGlobalSession();

// Or with custom settings
var desc = SlangGlobalSessionDesc.Default;
desc.EnableGLSL = true;
var customSession = Slang.CreateGlobalSession(desc);

// No need to manually dispose - COM reference counting handles cleanup
```

### Error Handling

```csharp
using SlangNet;
using System.Runtime.InteropServices;

try
{
    var session = Slang.CreateGlobalSession();
    // Use session...
}
catch (COMException ex)
{
    Console.WriteLine($"Slang error: {ex.Message}");
    Console.WriteLine($"HRESULT: 0x{ex.ErrorCode:X8}");
    
    // Get additional error details
    var errorMsg = Slang.GetLastInternalErrorMessage();
    if (errorMsg != null)
    {
        Console.WriteLine($"Details: {errorMsg}");
    }
}
```

## Examples

Check out the [examples](examples/) folder for complete working examples:

- **[HelloSlang](examples/HelloSlang/)** - Basic usage demonstrating session creation and configuration

To run an example:
```bash
cd examples/HelloSlang
dotnet run
```

## Project Structure

```
SlangNet/
├── SlangNet/                  # Main library project
│   ├── SlangNative.cs        # P/Invoke declarations
│   ├── IGlobalSession.cs     # Global session COM interface
│   ├── Slang.cs              # High-level wrapper class
│   └── AssemblyAttributes.cs # Runtime marshalling configuration
├── SlangNet.Test/            # Unit tests
├── examples/                 # Example projects
│   └── HelloSlang/          # Basic usage example
└── native-release/           # Native Slang binaries
    └── slang-2025.17.2-windows-x86_64/
```

## Native Dependencies

The NuGet package includes all required native DLLs for Windows x64:
- `slang.dll` - Main Slang compiler
- `slang-rt.dll` - Slang runtime library
- `slang-llvm.dll` - LLVM backend for code generation
- `slang-glslang.dll` - GLSL language support
- `slang-glsl-module.dll` - GLSL module
- `gfx.dll` - Graphics abstraction layer

These DLLs are automatically copied to the output directory and included in the NuGet package under `runtimes/win-x64/native/`.

## Building from Source

```bash
# Clone the repository
git clone https://github.com/Hong-Xiang/SlangNet.git
cd SlangNet

# Build the project
dotnet build SlangNet/SlangNet.csproj

# Run tests
dotnet test SlangNet.Test/SlangNet.Test.csproj

# Create NuGet package
dotnet pack SlangNet/SlangNet.csproj
```

## API Coverage

Currently implemented:
- ✅ `slang_createGlobalSession` - Create global session
- ✅ `slang_createGlobalSession2` - Create global session with descriptor
- ✅ `slang_shutdown` - Cleanup global allocations
- ✅ `slang_getLastInternalErrorMessage` - Get error messages

Coming soon:
- 🚧 Session creation and management
- 🚧 Module loading and compilation
- 🚧 Reflection API
- 🚧 Entry point management

## Slang Version

This binding is based on **Slang 2025.17.2**.

## License

MIT

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Links

- [Slang Official Website](https://shader-slang.com/)
- [Slang GitHub Repository](https://github.com/shader-slang/slang)

