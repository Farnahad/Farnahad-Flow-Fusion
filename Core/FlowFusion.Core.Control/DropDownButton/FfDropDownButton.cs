using System.Windows;

namespace FlowFusion.Core.Control.DropDownButton;

public class FfDropDownButton : DevExpress.Xpf.Core.DropDownButton
{
    public FfDropDownButton()
    {
        MinWidth = 75;
        Margin = new Thickness(2);
    }
}