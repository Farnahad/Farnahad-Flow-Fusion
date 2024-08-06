using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfPercentIntEditGridColumn : FfIntGridColumn
{
    public FfPercentIntEditGridColumn()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = false,
            Mask = "P0"
        };
    }
}