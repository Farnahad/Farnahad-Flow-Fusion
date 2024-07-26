using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffPercentDoubleEditGridColumn : FffDoubleEditGridColumn
{
    public FffPercentDoubleEditGridColumn()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = true,
            Mask = "P2"
        };
    }
}