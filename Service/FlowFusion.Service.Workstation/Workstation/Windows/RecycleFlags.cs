namespace FlowFusion.Service.Workstation.Workstation.Windows;

// RecycleFlags enumeration for flags used in SHEmptyRecycleBin
[Flags]
public enum RecycleFlags : uint
{
    SHERB_NOCONFIRMATION = 0x00000001, // No confirmation UI before emptying
    SHERB_NOPROGRESSUI = 0x00000002,  // No progress tracking UI during the emptying of the Recycle Bin
    SHERB_NOSOUND = 0x00000004         // No sound played when the Recycle Bin is emptied
}