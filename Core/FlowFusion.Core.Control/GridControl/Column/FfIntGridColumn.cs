using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfIntGridColumn : FfGridColumn
{
    public FfIntGridColumn()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = false,
            Mask = "N0",
            AllowNullInput = true,
            MinValue = 0M
        };
    }
}