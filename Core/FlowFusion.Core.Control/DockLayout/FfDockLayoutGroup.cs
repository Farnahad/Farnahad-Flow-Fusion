using System.Windows;
using DevExpress.Xpf.Docking;

namespace FarnahadFlowFusion.Core.Control.DockLayout;

public abstract class FfDockLayoutGroup : LayoutGroup
{
    public FfDockLayoutGroup()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
    }
}