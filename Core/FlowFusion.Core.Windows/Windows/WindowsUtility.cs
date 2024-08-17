using System.Diagnostics;
using System.Runtime.InteropServices;
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace FlowFusion.Core.Windows.Windows;

public class WindowsUtility
{
    private const uint SPI_GETSCREENSAVERRUNNING = 114;
    private const uint SPI_SETSCREENSAVEACTIVE = 17;
    private const uint SPI_GETSCREENSAVEACTIVE = 16;

    private const int WM_SYSCOMMAND = 0x0112;
    private const int SC_SCREENSAVE = 0xF140;


    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

    [DllImport("powrprof.dll", SetLastError = true)]
    private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    public static void EmptyRecycleBin()
    {
        try
        {
            var result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION);

            if (result != 0)
            {
                throw new Exception($"Failed to empty Recycle Bin. Error code: {result}");
            }
        }
        catch
        {
            // ignored
        }
    }

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
        var isRunning = IsScreenSaverRunning();
        if (isRunning)
        {
            SendMessage(IntPtr.Zero, WM_SYSCOMMAND, (IntPtr)SC_SCREENSAVE, IntPtr.Zero);
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

    public static async Task Hibernate()
    {
        var command = "shutdown /h";
        await ExecuteCommand(command);
    }

    public static async Task Restart(bool force)
    {
        var command = "shutdown /r" + (force ? " /f" : "") + " /t 0";
        await ExecuteCommand(command);
    }

    public static async Task Shutdown(bool force)
    {
        var command = "shutdown /s" + (force ? " /f" : "") + " /t 0";
        await ExecuteCommand(command);
    }

    public static void Sleep(bool force)
    {
        SetSuspendState(false, force, false);
    }

    private static async Task ExecuteCommand(string command)
    {
        var procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process
        {
            StartInfo = procStartInfo
        };

        process.Start();
        await process.WaitForExitAsync();
    }
}