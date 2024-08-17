using System.Drawing;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UseNameofExpression

namespace FlowFusion.Core.Windows.Screen;

public class ScreenUtility
{
    public const int ENUM_CURRENT_SETTINGS = -1;
    public const int CDS_UPDATEREGISTRY = 0x00000001;
    public const int CDS_TEST = 0x00000002;
    public const int DISP_CHANGE_SUCCESSFUL = 0;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, int dwflags, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

    // P/Invoke declarations for capturing the foreground window
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, out RECT rect);


    public static void SetScreenResolution(int monitorNumber, int monitorWidth, int monitorHeight,
        int monitorBitCount, int monitorFrequency)
    {
        var d = new DISPLAY_DEVICE();
        d.cb = Marshal.SizeOf(d);

        if (!ScreenUtility.EnumDisplayDevices(null, (uint)monitorNumber, ref d, 0))
            return;

        var dm = new DEVMODE();
        dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

        if (0 != ScreenUtility.ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ScreenUtility.ENUM_CURRENT_SETTINGS, IntPtr.Zero))
            return;

        dm.dmPelsWidth = monitorWidth;
        dm.dmPelsHeight = monitorHeight;
        dm.dmBitsPerPel = monitorBitCount;
        dm.dmDisplayFrequency = monitorFrequency;
        dm.dmFields = 0x400000 | 0x80000 | 0x200000 | 0x100000;

        var iRet = ScreenUtility.ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ScreenUtility.CDS_TEST, IntPtr.Zero);

        if (iRet == ScreenUtility.DISP_CHANGE_SUCCESSFUL)
        {
            ScreenUtility.ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, ScreenUtility.CDS_UPDATEREGISTRY, IntPtr.Zero);
        }
    }

    public static void GetScreenResolution(int monitorNumber, out int monitorWidth,
        out int monitorHeight, out int monitorBitCount, out int monitorFrequency)
    {
        try
        {
            var mi = new MONITORINFOEX();
            mi.cbSize = Marshal.SizeOf(mi);

            bool EnumMonitors(IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData)
            {
                mi = new MONITORINFOEX();
                mi.cbSize = Marshal.SizeOf(mi);
                GetMonitorInfo(hMonitor, ref mi);
                return true;
            }

            if (EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, EnumMonitors, IntPtr.Zero))
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











    //public void TakeScreenshot(ScreenshotCapture screenshotCapture, SaveScreenshotTo saveScreenshotTo, int screenToCapture)
    //{
    //    Bitmap screenshot = null;

    //    switch (screenshotCapture)
    //    {
    //        case ScreenshotCapture.AllScreens:
    //            screenshot = CaptureAllScreens();
    //            break;
    //        case ScreenshotCapture.ForegroundWindow:
    //            screenshot = CaptureForegroundWindow();
    //            break;
    //        case ScreenshotCapture.PrimaryScreen:
    //            screenshot = CapturePrimaryScreen();
    //            break;
    //        case ScreenshotCapture.SelectScreen:
    //            screenshot = CaptureSpecificScreen(screenToCapture);
    //            break;
    //    }

    //    if (screenshot != null)
    //    {
    //        if (saveScreenshotTo == SaveScreenshotTo.Clipboard)
    //        {
    //            Clipboard.SetImage(screenshot);
    //        }
    //        else if (saveScreenshotTo == SaveScreenshotTo.File)
    //        {
    //            SaveScreenshotToFile(screenshot);
    //        }
    //    }
    //}

    private Bitmap CaptureAllScreens()
    {
        Rectangle bounds = Screen.AllScreens[0].Bounds;
        foreach (var screen in Screen.AllScreens)
        {
            bounds = Rectangle.Union(bounds, screen.Bounds);
        }

        var bitmap = new Bitmap(bounds.Width, bounds.Height);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
        }

        return bitmap;
    }

    private Bitmap CaptureForegroundWindow()
    {
        var hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return null;

        GetWindowRect(hwnd, out var rect);
        var width = rect.Right - rect.Left;
        var height = rect.Bottom - rect.Top;

        var bitmap = new Bitmap(width, height);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
        }

        return bitmap;
    }

    public static Bitmap CapturePrimaryScreen()
    {
        Rectangle bounds = Screen.PrimaryScreen.Bounds;
        var bitmap = new Bitmap(bounds.Width, bounds.Height);
        using var g = Graphics.FromImage(bitmap);
        g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);

        return bitmap;
    }

    public static Bitmap CaptureSpecificScreen(int screenIndex)
    {
        if (screenIndex < 0 || screenIndex >= System.Windows.Forms.Screen.AllScreens.Length) return null;

        Rectangle bounds = Screen.AllScreens[screenIndex].Bounds;
        var bitmap = new Bitmap(bounds.Width, bounds.Height);
        using var g = Graphics.FromImage(bitmap);
        g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);

        return bitmap;
    }

    private void SaveScreenshotToFile(Bitmap screenshot)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "PNG Image|*.png",
            Title = "Save Screenshot"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            screenshot.Save(saveFileDialog.FileName, ImageFormat.Png);
        }
    }



}