using DevExpress.Xpf.Editors;

namespace FlowFusion.Core.Control.TextBox;

public class FfPhoneFaxMaskTextEdit : FfMaskTextEdit
{
    public FfPhoneFaxMaskTextEdit()
    {
        EditValueType = typeof(string);
        Mask = "99999999999";
        MaskType = MaskType.Simple;
    }
}