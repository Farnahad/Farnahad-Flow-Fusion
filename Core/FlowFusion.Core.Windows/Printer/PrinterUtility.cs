using System.Runtime.InteropServices;
using System.Text;
// ReSharper disable StringLiteralTypo

namespace FlowFusion.Core.Windows.Printer;

public class PrinterUtility
{
    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetDefaultPrinter(string pszPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

    public static void _SetDefaultPrinter(string printerName)
    {
        SetDefaultPrinter(printerName);
    }

    public static string GetDefaultPrinterName()
    {
        const int bufferSize = 256;
        var defaultPrinter = new StringBuilder(bufferSize);
        var bufferSizeOut = bufferSize;
        if (GetDefaultPrinter(defaultPrinter, ref bufferSizeOut))
        {
            return defaultPrinter.ToString();
        }
        else
        {
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }
    }

    public static bool PrintRawData(string printerName, string documentPath)
    {
        if (!File.Exists(documentPath))
        {
            return false;
        }

        var documentContent = File.ReadAllText(documentPath);

        var dwCount = documentContent.Length;
        var hPrinter = IntPtr.Zero;

        var di = new DOCINFOA
        {
            pDocName = "My C# Raw Document",
            pDataType = "RAW"
        };

        try
        {
            if (OpenPrinter(printerName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        var pBytes = Marshal.StringToCoTaskMemAnsi(documentContent);
                        var success = WritePrinter(hPrinter, pBytes, dwCount, out _);
                        Marshal.FreeCoTaskMem(pBytes);
                        EndPagePrinter(hPrinter);
                        if (!success)
                        {
                            return false;
                        }
                    }
                    EndDocPrinter(hPrinter);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        finally
        {
            ClosePrinter(hPrinter);
        }

        return true;
    }
}