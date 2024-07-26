using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffPercentIntEditGridColumn : FffIntGridColumn
{
    public FffPercentIntEditGridColumn()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = false,
            Mask = "P0"
        };
    }
}