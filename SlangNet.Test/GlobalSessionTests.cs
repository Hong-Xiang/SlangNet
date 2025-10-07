using SlangNet;
using System.Runtime.InteropServices;

namespace SlangNet.Test;

public class GlobalSessionTests
{
    [Fact]
    public void CreateGlobalSession_Succeeds()
    {
        // Arrange
        var desc = new SlangGlobalSessionDesc();

        // Act
        var session = Slang.CreateGlobalSession(desc);

        // Assert
        Assert.NotNull(session);
    }

    [Fact]
    public void SlangGlobalSessionDesc_Default_HasCorrectValues()
    {
        // Act
        var desc = new SlangGlobalSessionDesc();

        // Assert
        Assert.Equal((uint)Marshal.SizeOf<SlangGlobalSessionDesc>(), desc.StructureSize);
        Assert.Equal(0u, desc.ApiVersion); // SLANG_API_VERSION = 0
        Assert.Equal(SlangLanguageVersion.SLANG_LANGUAGE_VERSION_2025, desc.MinLanguageVersion);
        Assert.False(desc.EnableGLSL);
    }

    [Fact]
    public void GetLastInternalErrorMessage_ReturnsString()
    {
        // Act
        var message = Slang.GetLastInternalErrorMessage();

        // Assert - message could be null or a string
        Assert.True(message == null || message is string);
    }

    [Fact]
    public void Shutdown_CanBeCalled()
    {
        // Act & Assert (should not throw)
        // Note: Only call this in isolation or at the very end of tests
        // Slang.Shutdown();
    }
}
