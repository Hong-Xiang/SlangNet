namespace SlangNet.Native;

/// <summary>
/// Metadata for the Slang native binaries package.
/// </summary>
public static class SlangNativeMetadata
{
    /// <summary>
    /// The version of Slang native binaries included in this package.
    /// </summary>
    public const string Version = "2025.19.1";

    /// <summary>
    /// Supported runtime identifiers included in this package.
    /// </summary>
    public static readonly string[] SupportedRuntimes = new[]
    {
        "win-x64",
        "win-arm64",
        "linux-x64",
        "linux-arm64",
        "osx-x64",
        "osx-arm64"
    };

    /// <summary>
    /// URL to the Slang project.
    /// </summary>
    public const string ProjectUrl = "https://github.com/shader-slang/slang";

    /// <summary>
    /// Release notes URL.
    /// </summary>
    public const string ReleaseNotesUrl = "https://github.com/shader-slang/slang/releases/tag/v2025.19.1";
}

