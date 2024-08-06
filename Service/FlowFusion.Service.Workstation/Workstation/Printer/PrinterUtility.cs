using System.Runtime.InteropServices;
using System.Text;

namespace FarnahadFlowFusion.Service.Workstation.Workstation.Printer;

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
        Console.WriteLine(SetDefaultPrinter(printerName)
            ? $"Successfully set '{printerName}' as the default printer."
            : $"Failed to set '{printerName}' as the default printer.");
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
            Console.WriteLine("File not found: " + documentPath);
            return false;
        }

        var documentContent = File.ReadAllText(documentPath);

        IntPtr pBytes;
        var dwCount = documentContent.Length;
        var dwWritten = 0;
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
                        pBytes = Marshal.StringToCoTaskMemAnsi(documentContent);
                        var success = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        Marshal.FreeCoTaskMem(pBytes);
                        EndPagePrinter(hPrinter);
                        if (!success)
                        {
                            Console.WriteLine("Failed to write to printer.");
                            return false;
                        }
                    }
                    EndDocPrinter(hPrinter);
                }
                else
                {
                    Console.WriteLine("Failed to start document.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Failed to open printer.");
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