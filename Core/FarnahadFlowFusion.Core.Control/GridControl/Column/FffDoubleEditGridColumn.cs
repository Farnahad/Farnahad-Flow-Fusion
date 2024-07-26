using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffDoubleEditGridColumn : FffGridColumn
{
    public FffDoubleEditGridColumn()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = true,
            Mask = "N2"
        };
    }
}