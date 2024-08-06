using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadFlowFusion.Core.Control.Layout;

public class FfLayoutControl : LayoutControl
{
    public FfLayoutControl()
    {
        Orientation = Orientation.Vertical;
        AnimateScrolling = false;
        DragScrolling = false;
    }
}