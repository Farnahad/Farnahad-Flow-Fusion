using DevExpress.Utils;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using FlowFusion.Core.Control.Core;

namespace FlowFusion.Core.Control.GridControl.Column;

public abstract class FfGridColumn : GridColumn
{
    public FfGridColumn()
    {
        MinWidth = 40;
        AllowMoving = DefaultBoolean.True;
        AllowColumnFiltering = DefaultBoolean.False;
        EditSettings = new TextEditSettings();

        ControlBehavior.SetFfColumnWidth(this, FfColumnWidth.Star1);
    }
}