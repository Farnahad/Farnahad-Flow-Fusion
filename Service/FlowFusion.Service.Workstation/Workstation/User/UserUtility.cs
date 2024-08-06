using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace FlowFusion.Service.Workstation.Workstation.User;

public class UserUtility
{
    // Windows API constants
    private const int EWX_LOGOFF = 0x00000000; // Log off
    private const int EWX_FORCE = 0x00000004; // Force the logoff (ignores unsaved data)

    // Importing necessary Windows API function
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

    public static void LogoffUser()
    {
        try
        {
            var result = ExitWindowsEx(EWX_LOGOFF | EWX_FORCE, 0);
            if (!result)
            {
                var error = Marshal.GetLastWin32Error();
                throw new Exception($"Failed to log off user. Error code: {error}");
            }
            else
            {
                Console.WriteLine("User logged off successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error logging off user: {ex.Message}");
        }
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool LockWorkStation();

    public static void LockUser()
    {
        try
        {
            var result = LockWorkStation();
            if (!result)
            {
                var error = Marshal.GetLastWin32Error();
                throw new Exception($"Failed to lock Windows. Error code: {error}");
            }
            else
            {
                Console.WriteLine("Windows locked successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error locking Windows: {ex.Message}");
        }
    }
}