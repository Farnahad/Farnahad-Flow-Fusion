using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffCheckEditGridColumn : FffGridColumn
{
    public FffCheckEditGridColumn()
    {
        EditSettings = new CheckEditSettings();
    }
}