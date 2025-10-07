using SlangNet;
using System;
using System.Runtime.InteropServices;

Console.WriteLine("===========================================");
Console.WriteLine("   SlangNet - Hello Slang Example");
Console.WriteLine("===========================================");
Console.WriteLine();

try
{
    // Example 1: Create a global session with default settings
    Console.WriteLine("Example 1: Creating global session with default settings...");
    var session1 = Slang.CreateGlobalSession();
    Console.WriteLine("✓ Success: Global session created");
    Console.WriteLine();

    // Example 2: Create a global session with custom descriptor
    Console.WriteLine("Example 2: Creating global session with GLSL support...");
    var desc = SlangGlobalSessionDesc.Default;
    desc.EnableGLSL = true;
    desc.MinLanguageVersion = 2025;
    
    Console.WriteLine($"  Structure Size: {desc.StructureSize}");
    Console.WriteLine($"  API Version: {desc.ApiVersion}");
    Console.WriteLine($"  Min Language Version: {desc.MinLanguageVersion}");
    Console.WriteLine($"  Enable GLSL: {desc.EnableGLSL}");
    
    var session2 = Slang.CreateGlobalSession(desc);
    Console.WriteLine("✓ Success: Global session with GLSL support created");
    Console.WriteLine();

    // Example 3: Get error message API
    Console.WriteLine("Example 3: Checking error message API...");
    var errorMsg = Slang.GetLastInternalErrorMessage();
    Console.WriteLine($"  Last error message: {errorMsg ?? "(none)"}");
    Console.WriteLine("✓ Success: Error message API works");
    Console.WriteLine();

    Console.WriteLine("===========================================");
    Console.WriteLine("   All examples completed successfully!");
    Console.WriteLine("===========================================");
}
catch (COMException ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"✗ COM Error occurred:");
    Console.WriteLine($"  Message: {ex.Message}");
    Console.WriteLine($"  HRESULT: 0x{ex.ErrorCode:X8}");
    Console.ResetColor();
    
    var details = Slang.GetLastInternalErrorMessage();
    if (details != null)
    {
        Console.WriteLine($"  Details: {details}");
    }
    
    return 1;
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"✗ Unexpected error:");
    Console.WriteLine($"  Type: {ex.GetType().Name}");
    Console.WriteLine($"  Message: {ex.Message}");
    Console.ResetColor();
    return 1;
}

return 0;
