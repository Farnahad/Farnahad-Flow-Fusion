using System.Windows;
using DevExpress.Xpf.Core;

namespace FarnahadFlowFusion.Core.Control.Button;

public class FffSimpleButton : SimpleButton
{
    public FffSimpleButton()
    {
        MinWidth = 75;
        Margin = new Thickness(2);
    }
}