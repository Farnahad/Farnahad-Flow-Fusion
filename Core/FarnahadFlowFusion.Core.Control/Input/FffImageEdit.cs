using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.Input;

public class FffImageEdit : ImageEdit
{
    public FffImageEdit()
    {
        ShowMenu = false;
        ShowLoadDialogOnClickMode = ShowLoadDialogOnClickMode.Never;
    }
}