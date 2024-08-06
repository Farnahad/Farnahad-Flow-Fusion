using DevExpress.Xpf.Grid;

namespace FarnahadFlowFusion.Core.Control.GridControl;

public class FfReadingSearchableGridControl : FfReadingGridControl
{
    public FfReadingSearchableGridControl()
    {
        View.ShowSearchPanelMode = ShowSearchPanelMode.Always;
    }
}