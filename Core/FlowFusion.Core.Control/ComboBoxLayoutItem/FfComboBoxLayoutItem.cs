using System.Windows;

namespace FlowFusion.Core.Control.ComboBoxLayoutItem;

public partial class FfComboBoxLayoutItem
{
    public FfComboBoxLayoutItem()
    {
        InitializeComponent();
    }


    public object ItemsSource
    {
        get => (object)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object),
            typeof(FfComboBoxLayoutItem), new PropertyMetadata(null));
}