using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.List;

public class FfCheckedListBoxEdit : FfListBoxEdit
{
    public FfCheckedListBoxEdit()
    {
        StyleSettings = new CheckedListBoxEditStyleSettings();
    }
}