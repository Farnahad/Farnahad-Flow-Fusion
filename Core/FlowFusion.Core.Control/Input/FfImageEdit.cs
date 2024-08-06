using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.Input;

public class FfImageEdit : ImageEdit
{
    public FfImageEdit()
    {
        ShowMenu = false;
        ShowLoadDialogOnClickMode = ShowLoadDialogOnClickMode.Never;
    }
}