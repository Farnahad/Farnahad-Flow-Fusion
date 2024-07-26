using DevExpress.Xpf.Grid;

namespace FarnahadFlowFusion.Core.Control.GridControl;

public class FffReadingSearchableGridControl : FffReadingGridControl
{
    public FffReadingSearchableGridControl()
    {
        View.ShowSearchPanelMode = ShowSearchPanelMode.Always;
    }
}