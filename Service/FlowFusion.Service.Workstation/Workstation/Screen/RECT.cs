using System.Runtime.InteropServices;

namespace FlowFusion.Service.Workstation.Workstation.Screen;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}