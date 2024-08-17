// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
namespace FlowFusion.Core.Windows.Windows;

[Flags]
public enum RecycleFlags : uint
{
    SHERB_NOCONFIRMATION = 0x00000001,
    SHERB_NOPROGRESSUI = 0x00000002,
    SHERB_NOSOUND = 0x00000004
}