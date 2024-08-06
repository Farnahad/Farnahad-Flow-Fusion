using System.Windows;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadFlowFusion.Core.Control.Layout;

public abstract class FfLayoutGroup : LayoutGroup
{
    public FfLayoutGroup()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
    }
}