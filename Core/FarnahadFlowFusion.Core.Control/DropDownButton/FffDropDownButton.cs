using System.Windows;

namespace FarnahadFlowFusion.Core.Control.DropDownButton;

public class FffDropDownButton : DevExpress.Xpf.Core.DropDownButton
{
    public FffDropDownButton()
    {
        MinWidth = 75;
        Margin = new Thickness(2);
    }
}