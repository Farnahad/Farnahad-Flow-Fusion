using DevExpress.Xpf.Bars;

namespace FarnahadFlowFusion.Core.Control.Bar;

public class FffToolBarControl : ToolBarControl
{
    public FffToolBarControl()
    {
        ActualShowDragWidget = false;
        AllowCustomizationMenu = false;
        AllowQuickCustomization = false;
        BarItemDisplayMode = BarItemDisplayMode.ContentAndGlyph;
    }
}