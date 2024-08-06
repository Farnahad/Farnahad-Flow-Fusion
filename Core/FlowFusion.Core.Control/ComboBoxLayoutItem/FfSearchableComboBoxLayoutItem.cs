using System.Windows;

namespace FlowFusion.Core.Control.ComboBoxLayoutItem;

public partial class FfSearchableComboBoxLayoutItem
{
    public FfSearchableComboBoxLayoutItem()
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
            typeof(FfSearchableComboBoxLayoutItem), new PropertyMetadata(null));
}