using DevExpress.Xpf.Editors.Settings;

namespace FlowFusion.Core.Control.GridControl.Column;

public class FfCheckEditGridColumn : FfGridColumn
{
    public FfCheckEditGridColumn()
    {
        EditSettings = new CheckEditSettings();
    }
}