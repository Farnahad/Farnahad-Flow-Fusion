using DevExpress.Xpf.Grid;

namespace FarnahadFlowFusion.Core.Control.GridControl;

public class FffTableView : TableView
{
    public FffTableView()
    {
        ShowGroupPanel = false;
        AllowEditing = false;
        IsSynchronizedWithCurrentItem = true;
        AllowFixedColumnMenu = true;
        EnterMoveNextColumn = true;
        ShowEditFormOnF2Key = false;
        NavigationStyle = GridViewNavigationStyle.Row;
        AllowPerPixelScrolling = true;
        AllowBandMultiRow = true;
        ShowBandsPanel = false;
        UseEvenRowBackground = true;
        ImmediateUpdateRowPosition = false;
    }
}