
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SemanticKernelTour;

public static class Utils
{
    internal static void ShowImage(string filePath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", filePath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", filePath);
        }
        else
        {
            throw new NotSupportedException("Unsupported OS platform");
        }
    }
}