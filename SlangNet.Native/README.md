# SlangNet.Native

This package contains the native Slang shader compiler binaries for Windows x64.

## What's Included

This package automatically downloads and includes the following native files from the official Slang releases:

### DLLs (runtimes/win-x64/native/)
- `slang.dll` - Main Slang compiler library
- `slang-rt.dll` - Slang runtime library
- `slang-llvm.dll` - LLVM backend for code generation
- `slang-glslang.dll` - GLSL language support
- `slang-glsl-module.dll` - GLSL module
- `gfx.dll` - Graphics abstraction layer

### Executables
- `slangc.exe` - Slang command-line compiler
- `slangd.exe` - Slang language server
- `slangi.exe` - Slang interpreter

### Module Files
- `slang.slang` - Slang standard library
- `gfx.slang` - GFX library

## Version

This package contains **Slang v2025.19.1**

Official release: https://github.com/shader-slang/slang/releases/tag/v2025.19.1

## Usage

This is a dependency package that provides native binaries. It's typically referenced by higher-level packages like `SlangNet`.

### Direct Usage

```xml
<PackageReference Include="SlangNet.Native" Version="2025.19.1" />
```

The native binaries will be automatically copied to your output directory when building for Windows x64.

## Platform Support

- ✅ Windows x64 (win-x64)
- ❌ Linux (planned for future releases)
- ❌ macOS (planned for future releases)

## License

The native Slang binaries are licensed under the Apache 2.0 License.
See: https://github.com/shader-slang/slang/blob/master/LICENSE

This NuGet package wrapper is licensed under MIT.

## Build Process

This package uses MSBuild tasks to:
1. Download the official Slang release from GitHub
2. Extract the binaries
3. Package them in the correct NuGet structure (`runtimes/win-x64/native/`)

No binaries are checked into source control - they are downloaded at build time.

## Source

Binaries are downloaded from the official Slang releases:
https://github.com/shader-slang/slang/releases

## Related Packages

- **SlangNet** - .NET binding for Slang with COM interop

## Support

For issues with the native Slang binaries themselves, please report to:
https://github.com/shader-slang/slang/issues

For issues with this NuGet package, please report to:
https://github.com/Hong-Xiang/SlangNet/issues
