using System.Runtime.InteropServices;

namespace FlowFusion.Core.Windows.Screen;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
// ReSharper disable once IdentifierTypo
// ReSharper disable once InconsistentNaming
public struct MONITORINFOEX
{
    public int cbSize;
    public RECT rcMonitor;
    public RECT rcWork;
    public uint dwFlags;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string szDeviceName;
}