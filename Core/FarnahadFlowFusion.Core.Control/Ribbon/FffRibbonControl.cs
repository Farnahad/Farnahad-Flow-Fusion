using DevExpress.Xpf.Ribbon;

namespace FarnahadFlowFusion.Core.Control.Ribbon;

public class FffRibbonControl : RibbonControl
{
    public FffRibbonControl()
    {
        MenuIconStyle = RibbonMenuIconStyle.Office2013;
        RibbonStyle = RibbonStyle.Office2010;
        ApplicationButtonText = "File";
    }
}