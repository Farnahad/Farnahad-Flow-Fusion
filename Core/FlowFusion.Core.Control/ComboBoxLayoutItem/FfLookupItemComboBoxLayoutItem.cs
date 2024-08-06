using System.Windows;

namespace FarnahadFlowFusion.Core.Control.ComboBoxLayoutItem;

public partial class FfLookupItemComboBoxLayoutItem
{
    public FfLookupItemComboBoxLayoutItem()
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
            typeof(FfLookupItemComboBoxLayoutItem), new PropertyMetadata(null));
}