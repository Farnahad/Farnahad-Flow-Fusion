using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfPercentDoubleEditGridColumn : FfDoubleEditGridColumn
{
    public FfPercentDoubleEditGridColumn()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = true,
            Mask = "P2"
        };
    }
}