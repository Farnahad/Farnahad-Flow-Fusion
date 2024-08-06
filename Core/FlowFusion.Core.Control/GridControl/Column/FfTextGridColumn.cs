using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfTextGridColumn : FfGridColumn
{
    public FfTextGridColumn()
    {
        EditSettings = new TextEditSettings();
    }
}