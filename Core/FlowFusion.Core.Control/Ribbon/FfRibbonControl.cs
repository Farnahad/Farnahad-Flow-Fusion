using DevExpress.Xpf.Ribbon;

namespace FarnahadFlowFusion.Core.Control.Ribbon;

public class FfRibbonControl : RibbonControl
{
    public FfRibbonControl()
    {
        MenuIconStyle = RibbonMenuIconStyle.Office2013;
        RibbonStyle = RibbonStyle.Office2010;
        ApplicationButtonText = "File";
    }
}