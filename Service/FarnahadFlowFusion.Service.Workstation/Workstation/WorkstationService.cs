using System.Diagnostics;
using System.Runtime.InteropServices;
using FarnahadFlowFusion.Service.Workstation.Workstation.Printer;
using FarnahadFlowFusion.Service.Workstation.Workstation.Screen;
using FarnahadFlowFusion.Service.Workstation.Workstation.User;
using FarnahadFlowFusion.Service.Workstation.Workstation.Windows;

namespace FarnahadFlowFusion.Service.Workstation.Workstation;

public class WorkstationService
{
    public void DisableScreenSaver()
    {
        WindowsUtility.DisableScreenSaver();
    }

    public void EnableScreenSaver()
    {
        WindowsUtility.EnableScreenSaver();
    }

    public void StartScreenSaver()
    {
        WindowsUtility.StartScreenSaver();
    }

    public void StopScreenSaver()
    {
        WindowsUtility.StopScreenSaver();
    }

    public void LockUser()
    {
        UserUtility.LockUser();
    }

    public void LogoffUser()
    {
        UserUtility.LogoffUser();
    }

    public void SetScreenResolution(int monitorNumber, int monitorWidth, int monitorHeight,
            int monitorBitCount, int monitorFrequency)
    {
        var d = new DISPLAY_DEVICE();
        d.cb = Marshal.SizeOf(d);

        if (!ScreenUtility.EnumDisplayDevices(null, (uint)monitorNumber, ref d, 0))
        {
            Console.WriteLine("Error: Cannot find monitor.");
            return;
        }

        var dm = new DEVMODE();
        dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

        if (0 != ScreenUtility.ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ScreenUtility.ENUM_CURRENT_SETTINGS, IntPtr.Zero))
        {
            Console.WriteLine("Error: Cannot enumerate display settings.");
            return;
        }

        dm.dmPelsWidth = monitorWidth;
        dm.dmPelsHeight = monitorHeight;
        dm.dmBitsPerPel = monitorBitCount;
        dm.dmDisplayFrequency = monitorFrequency;
        dm.dmFields = 0x400000 | 0x80000 | 0x200000 | 0x100000;

        var iRet = ScreenUtility.ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ScreenUtility.CDS_TEST, IntPtr.Zero);

        if (iRet == ScreenUtility.DISP_CHANGE_SUCCESSFUL)
        {
            ScreenUtility.ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ScreenUtility.CDS_UPDATEREGISTRY, IntPtr.Zero);
            Console.WriteLine("Display settings changed successfully.");
        }
        else
        {
            Console.WriteLine("Error: Unable to change display settings.");
        }
    }

    public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

    public void GetScreenResolution(int monitorNumber, out int monitorWidth,
        out int monitorHeight, out int monitorBitCount, out int monitorFrequency)
    {
        try
        {
            var mi = new MONITORINFOEX();
            mi.cbSize = Marshal.SizeOf(mi);

            MonitorEnumDelegate enumMonitors = (IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData) =>
            {
                mi = new MONITORINFOEX();
                mi.cbSize = Marshal.SizeOf(mi);
                GetMonitorInfo(hMonitor, ref mi);
                return true;
            };

            if (EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, enumMonitors, IntPtr.Zero))
            {
                if (monitorNumber >= 0)
                {
                    var dm = new DEVMODE();
                    dm.dmSize = (short)Marshal.SizeOf(dm);
                    dm.dmDeviceName = mi.szDeviceName;

                    if (EnumDisplaySettings(dm.dmDeviceName, -1, ref dm))
                    {
                        monitorWidth = dm.dmPelsWidth;
                        monitorHeight = dm.dmPelsHeight;
                        monitorBitCount = dm.dmBitsPerPel;
                        monitorFrequency = dm.dmDisplayFrequency;
                    }
                    else
                    {
                        throw new Exception("Failed to retrieve display settings.");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("monitorNumber", "Monitor number must be >= 0.");
                }
            }
            else
            {
                throw new Exception("Failed to enumerate display monitors.");
            }
        }
        catch (Exception ex)
        {
            monitorWidth = 0;
            monitorHeight = 0;
            monitorBitCount = 0;
            monitorFrequency = 0;
        }
    }

    public void SetDefaultPrinter(string printerName)
    {
        PrinterUtility._SetDefaultPrinter(printerName);
    }

    public void PrintDocument(string documentPath)
    {
        try
        {
            var defaultPrinter = GetDefaultPrinter();

            if (PrinterUtility.PrintRawData(defaultPrinter, documentPath))
            {
                Console.WriteLine("Printing document: " + documentPath + " to printer: " + defaultPrinter);
            }
            else
            {
                Console.WriteLine("Error printing document.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public string GetDefaultPrinter()
    {
        return PrinterUtility.GetDefaultPrinterName();
    }

    public void EmptyRecycleBin()
    {
        WindowsUtility.EmptyRecycleBin();
    }

    public async Task Hibernate()
    {
        var command = "shutdown /h";
        await ExecuteCommand(command);
    }

    public async Task Restart(bool force)
    {
        var command = "shutdown /r" + (force ? " /f" : "") + " /t 0";
        await ExecuteCommand(command);
    }

    public async Task Shutdown(bool force)
    {
        var command = "shutdown /s" + (force ? " /f" : "") + " /t 0";
        await ExecuteCommand(command);
    }

    [System.Runtime.InteropServices.DllImport("powrprof.dll", SetLastError = true)]
    private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

    public void Sleep(bool force)
    {
        SetSuspendState(false, force, false);
    }

    private async Task ExecuteCommand(string command)
    {
        var procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var proc = new Process
        {
            StartInfo = procStartInfo
        };

        proc.Start();
        await proc.WaitForExitAsync();
    }
}