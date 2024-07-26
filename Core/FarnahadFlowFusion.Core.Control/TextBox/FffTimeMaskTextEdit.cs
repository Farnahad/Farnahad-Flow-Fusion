using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FffTimeMaskTextEdit : FffMaskTextEdit
{
    public FffTimeMaskTextEdit()
    {
        EditValueType = typeof(System.DateTime);
        MaskType = MaskType.DateTime;
        Mask = "HH:mm";
        MaskUseAsDisplayFormat = true;
    }
}