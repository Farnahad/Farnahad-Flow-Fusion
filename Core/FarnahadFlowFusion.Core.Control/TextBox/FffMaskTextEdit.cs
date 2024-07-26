using DevExpress.Xpf.Editors.Validation;

namespace FarnahadFlowFusion.Core.Control.TextBox;

public abstract class FffMaskTextEdit : FffTextEdit
{
    public FffMaskTextEdit()
    {
        NullText = "Empty";
        MaskUseAsDisplayFormat = true;
        MaskIgnoreBlank = false;
        InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
    }
}