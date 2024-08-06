using System.Windows;
using System.Windows.Controls;

namespace FlowFusion.Core.Control.TextBox;

public class FfLargeTextEdit : FfTextEdit
{
    public FfLargeTextEdit()
    {
        VerticalContentAlignment = VerticalAlignment.Top;
        AcceptsReturn = true;
        TextWrapping = TextWrapping.Wrap;
        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        Loaded += FfLargeTextEdit_Loaded;

        MinHeight = 100;
    }

    private void FfLargeTextEdit_Loaded(object sender, RoutedEventArgs e)
    {
        MaxWidth = ActualWidth;
        MaxHeight = ActualHeight;
    }
}