using System.Runtime.InteropServices;

namespace FlowFusion.Service.Workstation.Workstation.Printer;

// P/Invoke Declarations (from previous example)
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public class DOCINFOA
{
    [MarshalAs(UnmanagedType.LPStr)]
    public string pDocName;
    [MarshalAs(UnmanagedType.LPStr)]
    public string pOutputFile;
    [MarshalAs(UnmanagedType.LPStr)]
    public string pDataType;
}