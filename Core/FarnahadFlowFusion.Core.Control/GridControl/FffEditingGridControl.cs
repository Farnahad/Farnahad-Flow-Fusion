using DevExpress.Xpf.Grid;

namespace FarnahadFlowFusion.Core.Control.GridControl;

public class FffEditingGridControl : FffGridControl
{
    public FffEditingGridControl()
    {
        var fffTableView = (FffTableView)View;
        fffTableView.AllowEditing = true;
        fffTableView.NavigationStyle = GridViewNavigationStyle.Cell;
    }
}