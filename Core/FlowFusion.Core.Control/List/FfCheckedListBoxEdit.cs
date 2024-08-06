using DevExpress.Xpf.Editors;

namespace FlowFusion.Core.Control.List;

public class FfCheckedListBoxEdit : FfListBoxEdit
{
    public FfCheckedListBoxEdit()
    {
        StyleSettings = new CheckedListBoxEditStyleSettings();
    }
}