using DevExpress.Xpf.Grid;

namespace FarnahadFlowFusion.Core.Control.GridControl;

public class FfEditingGridControl : FfGridControl
{
    public FfEditingGridControl()
    {
        var ffTableView = (FfTableView)View;
        ffTableView.AllowEditing = true;
        ffTableView.NavigationStyle = GridViewNavigationStyle.Cell;
    }
}