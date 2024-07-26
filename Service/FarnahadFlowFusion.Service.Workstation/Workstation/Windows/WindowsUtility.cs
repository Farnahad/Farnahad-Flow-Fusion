using System.Runtime.InteropServices;

namespace FarnahadFlowFusion.Service.Workstation.Workstation.Windows;

public class WindowsUtility
{
    // Import SHEmptyRecycleBin function from shell32.dll
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

    public static void EmptyRecycleBin()
    {
        try
        {
            // Call SHEmptyRecycleBin with null parameters to empty all Recycle Bins
            var result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION);

            if (result == 0)
            {
                Console.WriteLine("Recycle Bin emptied successfully.");
            }
            else
            {
                throw new Exception($"Failed to empty Recycle Bin. Error code: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error emptying Recycle Bin: {ex.Message}");
        }
    }

    // Import SystemParametersInfo function from user32.dll
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

    // SystemParametersInfo constants
    private const uint SPI_GETSCREENSAVERRUNNING = 114;
    private const uint SPI_SETSCREENSAVEACTIVE = 17;
    private const uint SPI_GETSCREENSAVEACTIVE = 16;

    // Import SendMessage function from user32.dll
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    // SendMessage constants
    private const int WM_SYSCOMMAND = 0x0112;
    private const int SC_SCREENSAVE = 0xF140;

    public static void DisableScreenSaver()
    {
        SystemParametersInfo(SPI_SETSCREENSAVEACTIVE, 0, IntPtr.Zero, 0);
    }

    public static void EnableScreenSaver()
    {
        SystemParametersInfo(SPI_SETSCREENSAVEACTIVE, 1, IntPtr.Zero, 0);
    }

    public static void StartScreenSaver()
    {
        SendMessage(IntPtr.Zero, WM_SYSCOMMAND, (IntPtr)SC_SCREENSAVE, IntPtr.Zero);
    }

    public static void StopScreenSaver()
    {
        // Stop screen saver
        var isRunning = IsScreenSaverRunning();
        if (isRunning)
        {
            SendMessage(IntPtr.Zero, WM_SYSCOMMAND, (IntPtr)SC_SCREENSAVE, IntPtr.Zero);
            Console.WriteLine("Screen saver stopped.");
        }
        else
        {
            Console.WriteLine("Screen saver is not currently running.");
        }
    }

    private static bool IsScreenSaverRunning()
    {
        var runningPtr = Marshal.AllocHGlobal(sizeof(uint));
        var result = SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0, runningPtr, 0);
        var running = Marshal.ReadInt32(runningPtr) != 0;
        Marshal.FreeHGlobal(runningPtr);
        return running && result;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

    [DllImport("powrprof.dll", SetLastError = true)]
    private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

    public static void Hibernate()
    {
        SetSuspendState(true, true, true);
    }

    public static void Restart()
    {
        ExitWindowsEx(2, 0);
    }

    public static void Shutdown()
    {
        ExitWindowsEx(1, 0);
    }

    public static void Sleep()
    {
        SetSuspendState(false, true, true);
    }
}