using System.Windows;
using DevExpress.Xpf.Core;

namespace FlowFusion.Core.Control.Button;

public class FfSimpleButton : SimpleButton
{
    public FfSimpleButton()
    {
        MinWidth = 75;
        Margin = new Thickness(2);
    }
}