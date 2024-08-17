using System.Runtime.InteropServices;

namespace FlowFusion.Core.Windows.Screen;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
// ReSharper disable once InconsistentNaming
public struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}