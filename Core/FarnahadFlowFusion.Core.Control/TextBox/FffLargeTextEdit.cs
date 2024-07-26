using System.Windows;
using System.Windows.Controls;

namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FffLargeTextEdit : FffTextEdit
{
    public FffLargeTextEdit()
    {
        VerticalContentAlignment = VerticalAlignment.Top;
        AcceptsReturn = true;
        TextWrapping = TextWrapping.Wrap;
        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        Loaded += FffLargeTextEdit_Loaded;

        MinHeight = 100;
    }

    private void FffLargeTextEdit_Loaded(object sender, RoutedEventArgs e)
    {
        MaxWidth = ActualWidth;
        MaxHeight = ActualHeight;
    }
}