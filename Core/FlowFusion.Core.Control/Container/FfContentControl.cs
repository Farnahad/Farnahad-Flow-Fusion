using System.Windows;
using System.Windows.Controls;

namespace FarnahadFlowFusion.Core.Control.Container;

public class FfContentControl : ContentControl
{
    public FfContentControl()
    {
        VerticalContentAlignment = VerticalAlignment.Stretch;
        HorizontalContentAlignment = HorizontalAlignment.Stretch;
    }
}