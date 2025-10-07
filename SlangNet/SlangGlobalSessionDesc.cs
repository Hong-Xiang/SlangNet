using System.Runtime.InteropServices;

namespace SlangNet;

/// <summary>
/// Description of a Slang global session.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SlangGlobalSessionDesc
{
    /// <summary>
    /// Size of this struct.
    /// </summary>
    public uint StructureSize = (uint)sizeof(SlangGlobalSessionDesc);

    /// <summary>
    /// Slang API version.
    /// </summary>
    public uint ApiVersion = 0;

    /// <summary>
    /// Specify the oldest Slang language version that any sessions will use.
    /// </summary>
    public SlangLanguageVersion MinLanguageVersion = SlangLanguageVersion.SLANG_LANGUAGE_VERSION_2025;

    /// <summary>
    /// Whether to enable GLSL support.
    /// </summary>
    /// 
    public bool EnableGLSL = false;

    // Reserved for future use
    private fixed byte reserved[16];

    public SlangGlobalSessionDesc()
    {
    }
}
