using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadFlowFusion.Core.Control.Layout;

public class FffLayoutControl : LayoutControl
{
    public FffLayoutControl()
    {
        Orientation = Orientation.Vertical;
        AnimateScrolling = false;
        DragScrolling = false;
    }
}