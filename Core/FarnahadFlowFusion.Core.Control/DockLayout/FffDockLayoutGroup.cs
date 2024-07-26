using System.Windows;
using DevExpress.Xpf.Docking;

namespace FarnahadFlowFusion.Core.Control.DockLayout;

public abstract class FffDockLayoutGroup : LayoutGroup
{
    public FffDockLayoutGroup()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
    }
}