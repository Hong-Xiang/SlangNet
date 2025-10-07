using System.Runtime.InteropServices;

namespace SlangNet;

/// <summary>
/// P/Invoke declarations for Slang native library.
/// </summary>
internal static partial class SlangNative
{
    private const string DllName = "slang";

    /// <summary>
    /// API version constant.
    /// </summary>
    public const int ApiVersion = 0;

    /// <summary>
    /// Create a global session with the built-in core module.
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION (0)</param>
    /// <param name="outGlobalSession">The created global session</param>
    /// <returns>HRESULT</returns>
    [LibraryImport(DllName, EntryPoint = "slang_createGlobalSession")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int CreateGlobalSession(
        nint apiVersion,
        out IntPtr outGlobalSession);

    /// <summary>
    /// Create a global session with descriptor.
    /// </summary>
    /// <param name="desc">Description of the global session</param>
    /// <param name="outGlobalSession">The created global session</param>
    /// <returns>HRESULT</returns>
    [LibraryImport(DllName, EntryPoint = "slang_createGlobalSession2")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int CreateGlobalSession2(
        in SlangGlobalSessionDesc desc,
        out IntPtr outGlobalSession);

    /// <summary>
    /// Create a global session without the built-in core module.
    /// The core module can then be loaded via loadCoreModule or compileCoreModule.
    /// NOTE: API is experimental and not ready for production code.
    /// </summary>
    /// <param name="apiVersion">Pass in SLANG_API_VERSION (0)</param>
    /// <param name="outGlobalSession">The created global session</param>
    /// <returns>HRESULT</returns>
    [LibraryImport(DllName, EntryPoint = "slang_createGlobalSessionWithoutCoreModule")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int CreateGlobalSessionWithoutCoreModule(
        nint apiVersion,
        out IntPtr outGlobalSession);

    /// <summary>
    /// Cleanup all global allocations used by Slang.
    /// This function should only be called after all Slang objects have been released.
    /// </summary>
    [LibraryImport(DllName, EntryPoint = "slang_shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void Shutdown();

    /// <summary>
    /// Return the last signaled internal error message.
    /// </summary>
    [LibraryImport(DllName, EntryPoint = "slang_getLastInternalErrorMessage")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr GetLastInternalErrorMessage();

    /// <summary>
    /// Create a blob with given data.
    /// </summary>
    /// <param name="data">Pointer to the data</param>
    /// <param name="size">Size of the data in bytes</param>
    /// <param name="outBlob">The created blob</param>
    /// <returns>HRESULT</returns>
    [LibraryImport(DllName, EntryPoint = "slang_createBlob")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int CreateBlob(
        IntPtr data,
        nuint size,
        out IntPtr outBlob);

    /// <summary>
    /// Create a bytecode runner.
    /// </summary>
    /// <param name="outRunner">The created bytecode runner</param>
    /// <returns>HRESULT</returns>
    [LibraryImport(DllName, EntryPoint = "slang_createByteCodeRunner")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int CreateByteCodeRunner(out IntPtr outRunner);

    /// <summary>
    /// Disassemble bytecode to a human-readable format.
    /// </summary>
    /// <param name="byteCode">Pointer to the bytecode data</param>
    /// <param name="byteCodeSize">Size of the bytecode in bytes</param>
    /// <param name="outDisassembly">The disassembled output blob</param>
    /// <returns>HRESULT</returns>
    [LibraryImport(DllName, EntryPoint = "slang_disassembleByteCode")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int DisassembleByteCode(
        IntPtr byteCode,
        nuint byteCodeSize,
        out IntPtr outDisassembly);

    /// <summary>
    /// Load a module from source code.
    /// </summary>
    /// <param name="session">The session to load the module into</param>
    /// <param name="moduleName">The name of the module</param>
    /// <param name="path">The path for the module</param>
    /// <param name="source">Pointer to the source code data</param>
    /// <param name="sourceSize">Size of the source code data in bytes</param>
    /// <param name="outDiagnostics">Optional diagnostics output</param>
    /// <returns>Pointer to the loaded module, or null on failure</returns>
    [LibraryImport(DllName, EntryPoint = "slang_loadModuleFromSource", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr LoadModuleFromSource(
        IntPtr session,
        string moduleName,
        string path,
        IntPtr source,
        nuint sourceSize,
        IntPtr outDiagnostics);

    /// <summary>
    /// Load a module from IR (intermediate representation) blob.
    /// </summary>
    /// <param name="session">The session to load the module into</param>
    /// <param name="moduleName">Name of the module to load</param>
    /// <param name="path">Path for the module (used for diagnostics)</param>
    /// <param name="source">IR data containing the module</param>
    /// <param name="sourceSize">Size of the IR data in bytes</param>
    /// <param name="outDiagnostics">Optional diagnostics output</param>
    /// <returns>Pointer to the loaded module, or null on failure</returns>
    [LibraryImport(DllName, EntryPoint = "slang_loadModuleFromIRBlob", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr LoadModuleFromIRBlob(
        IntPtr session,
        string moduleName,
        string path,
        IntPtr source,
        nuint sourceSize,
        IntPtr outDiagnostics);

    /// <summary>
    /// Read module info (name and version) from IR blob.
    /// </summary>
    /// <param name="session">The session to use for loading module info</param>
    /// <param name="source">IR data containing the module</param>
    /// <param name="sourceSize">Size of the IR data in bytes</param>
    /// <param name="outModuleVersion">Module version number</param>
    /// <param name="outModuleCompilerVersion">Compiler version that created the module</param>
    /// <param name="outModuleName">Name of the module</param>
    /// <returns>HRESULT</returns>
    [LibraryImport(DllName, EntryPoint = "slang_loadModuleInfoFromIRBlob")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int LoadModuleInfoFromIRBlob(
        IntPtr session,
        IntPtr source,
        nuint sourceSize,
        out nint outModuleVersion,
        out IntPtr outModuleCompilerVersion,
        out IntPtr outModuleName);

    /// <summary>
    /// Get the last internal error message as a managed string.
    /// </summary>
    public static string? GetLastInternalErrorMessageString()
    {
        var ptr = GetLastInternalErrorMessage();
        return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : null;
    }
}
