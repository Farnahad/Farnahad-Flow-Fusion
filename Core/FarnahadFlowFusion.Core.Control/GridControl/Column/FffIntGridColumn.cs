using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffIntGridColumn : FffGridColumn
{
    public FffIntGridColumn()
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