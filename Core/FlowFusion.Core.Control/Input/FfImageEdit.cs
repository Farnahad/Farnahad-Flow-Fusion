using DevExpress.Xpf.Editors;

namespace FlowFusion.Core.Control.Input;

public class FfImageEdit : ImageEdit
{
    public FfImageEdit()
    {
        ShowMenu = false;
        ShowLoadDialogOnClickMode = ShowLoadDialogOnClickMode.Never;
    }
}