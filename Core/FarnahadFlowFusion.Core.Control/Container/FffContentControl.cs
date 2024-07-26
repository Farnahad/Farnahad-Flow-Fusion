using System.Windows;
using System.Windows.Controls;

namespace FarnahadFlowFusion.Core.Control.Container;

public class FffContentControl : ContentControl
{
    public FffContentControl()
    {
        VerticalContentAlignment = VerticalAlignment.Stretch;
        HorizontalContentAlignment = HorizontalAlignment.Stretch;
    }
}