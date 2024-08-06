using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FfTimeMaskTextEdit : FfMaskTextEdit
{
    public FfTimeMaskTextEdit()
    {
        EditValueType = typeof(System.DateTime);
        MaskType = MaskType.DateTime;
        Mask = "HH:mm";
        MaskUseAsDisplayFormat = true;
    }
}