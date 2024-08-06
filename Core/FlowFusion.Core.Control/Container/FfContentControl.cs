using System.Windows;
using System.Windows.Controls;

namespace FlowFusion.Core.Control.Container;

public class FfContentControl : ContentControl
{
    public FfContentControl()
    {
        VerticalContentAlignment = VerticalAlignment.Stretch;
        HorizontalContentAlignment = HorizontalAlignment.Stretch;
    }
}