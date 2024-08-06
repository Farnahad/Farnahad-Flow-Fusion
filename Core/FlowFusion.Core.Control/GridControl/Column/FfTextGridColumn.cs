using DevExpress.Xpf.Editors.Settings;

namespace FlowFusion.Core.Control.GridControl.Column;

public class FfTextGridColumn : FfGridColumn
{
    public FfTextGridColumn()
    {
        EditSettings = new TextEditSettings();
    }
}