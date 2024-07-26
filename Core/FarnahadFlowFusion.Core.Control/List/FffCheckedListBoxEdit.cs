using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.List;

public class FffCheckedListBoxEdit : FffListBoxEdit
{
    public FffCheckedListBoxEdit()
    {
        StyleSettings = new CheckedListBoxEditStyleSettings();
    }
}