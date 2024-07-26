using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffTextGridColumn : FffGridColumn
{
    public FffTextGridColumn()
    {
        EditSettings = new TextEditSettings();
    }
}