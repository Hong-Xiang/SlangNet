using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SlangNet;

/// <summary>
/// High-level wrapper for Slang API.
/// </summary>
public static class Slang
{
    private static readonly StrategyBasedComWrappers s_comWrappers = new();

    /// <summary>
    /// Create a global session with custom descriptor.
    /// </summary>
    /// <param name="desc">Description of the global session</param>
    /// <returns>The created global session</returns>
    public static IGlobalSession CreateGlobalSession(SlangGlobalSessionDesc desc)
    {
        IntPtr ptr;
        var hr = SlangNative.CreateGlobalSession2(in desc, out ptr);
        Marshal.ThrowExceptionForHR(hr);
        var session = (IGlobalSession)s_comWrappers.GetOrCreateObjectForComInstance(ptr, CreateObjectFlags.None);
        Marshal.Release(ptr);
        return session;
    }

    /// <summary>
    /// Cleanup all global allocations used by Slang.
    /// This function should only be called after all Slang objects have been released.
    /// </summary>
    public static void Shutdown()
    {
        SlangNative.Shutdown();
    }

    /// <summary>
    /// Get the last internal error message.
    /// </summary>
    public static string? GetLastInternalErrorMessage()
    {
        return SlangNative.GetLastInternalErrorMessageString();
    }
}