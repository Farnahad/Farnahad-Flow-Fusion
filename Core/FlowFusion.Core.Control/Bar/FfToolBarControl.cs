using DevExpress.Xpf.Bars;

namespace FlowFusion.Core.Control.Bar;

public class FfToolBarControl : ToolBarControl
{
    public FfToolBarControl()
    {
        ActualShowDragWidget = false;
        AllowCustomizationMenu = false;
        AllowQuickCustomization = false;
        BarItemDisplayMode = BarItemDisplayMode.ContentAndGlyph;
    }
}