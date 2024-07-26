using System.Windows;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadFlowFusion.Core.Control.Layout;

public abstract class FffLayoutGroup : LayoutGroup
{
    public FffLayoutGroup()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
    }
}