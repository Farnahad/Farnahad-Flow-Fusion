using DevExpress.Xpf.Editors;

namespace FlowFusion.Core.Control.TextBox;

public abstract class FfSpinEdit : SpinEdit
{
    public FfSpinEdit()
    {
        AllowNullInput = true;
        NullText = "Empty";
        MaskUseAsDisplayFormat = true;
        ShowEditorButtons = true;
    }
}