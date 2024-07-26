using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.TextBox;

public abstract class FffSpinEdit : SpinEdit
{
    public FffSpinEdit()
    {
        AllowNullInput = true;
        NullText = "Empty";
        MaskUseAsDisplayFormat = true;
        ShowEditorButtons = true;
    }
}