using DevExpress.Utils;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using FarnahadFlowFusion.Core.Control.Core;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public abstract class FffGridColumn : GridColumn
{
    public FffGridColumn()
    {
        MinWidth = 40;
        AllowMoving = DefaultBoolean.True;
        AllowColumnFiltering = DefaultBoolean.False;
        EditSettings = new TextEditSettings();

        ControlBehavior.SetFffColumnWidth(this, FffColumnWidth.Star1);
    }
}