using DevExpress.Xpf.Editors;

namespace FlowFusion.Core.Control.ComboBox;

public class FfMultiSearchableComboBoxEdit : FfSearchableComboBoxEdit
{
    public FfMultiSearchableComboBoxEdit()
    {
        StyleSettings = new CheckedComboBoxStyleSettings();
    }
}