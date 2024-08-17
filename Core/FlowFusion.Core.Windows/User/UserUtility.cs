using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
namespace FlowFusion.Core.Windows.User;

public class UserUtility
{
    private const int EWX_LOGOFF = 0x00000000;
    private const int EWX_FORCE = 0x00000004;

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
        }
        catch (Exception ex)
        {
            // ignored
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
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
}