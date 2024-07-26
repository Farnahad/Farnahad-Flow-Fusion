using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FffPhoneFaxMaskTextEdit : FffMaskTextEdit
{
    public FffPhoneFaxMaskTextEdit()
    {
        EditValueType = typeof(string);
        Mask = "99999999999";
        MaskType = MaskType.Simple;
    }
}