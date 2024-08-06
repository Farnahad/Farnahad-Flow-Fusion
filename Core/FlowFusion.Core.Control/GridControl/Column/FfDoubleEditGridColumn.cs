using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfDoubleEditGridColumn : FfGridColumn
{
    public FfDoubleEditGridColumn()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = true,
            Mask = "N2"
        };
    }
}