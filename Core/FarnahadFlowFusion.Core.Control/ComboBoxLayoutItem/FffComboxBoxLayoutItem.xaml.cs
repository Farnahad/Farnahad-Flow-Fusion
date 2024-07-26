using System.Windows;

namespace FarnahadFlowFusion.Core.Control.ComboBoxLayoutItem;

public partial class FffComboBoxLayoutItem
{
    public FffComboBoxLayoutItem()
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
            typeof(FffComboBoxLayoutItem), new PropertyMetadata(null));
}