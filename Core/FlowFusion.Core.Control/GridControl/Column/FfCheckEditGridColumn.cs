using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfCheckEditGridColumn : FfGridColumn
{
    public FfCheckEditGridColumn()
    {
        EditSettings = new CheckEditSettings();
    }
}