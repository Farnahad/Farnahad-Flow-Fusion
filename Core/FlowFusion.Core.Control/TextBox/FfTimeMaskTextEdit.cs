using DevExpress.Xpf.Editors;

namespace FlowFusion.Core.Control.TextBox;

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