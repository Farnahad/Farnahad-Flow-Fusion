using System.Windows;

namespace FarnahadFlowFusion.Core.Control.DockLayout;

public static partial class ControlBehavior
{
    public static FfDockType? GetFfDockType(DependencyObject dependencyObject)
    {
        return (FfDockType?)dependencyObject.GetValue(FfDockTypeProperty);
    }

    public static void SetFfDockType(DependencyObject dependencyObject, FfDockType? value)
    {
        dependencyObject.SetValue(FfDockTypeProperty, value);
    }

    public static readonly DependencyProperty FfDockTypeProperty =
        DependencyProperty.RegisterAttached("FfDockType", typeof(FfDockType?),
            typeof(ControlBehavior), new PropertyMetadata(null));


    public static FfDockPotion? GetFfDockPotion(DependencyObject dependencyObject)
    {
        return (FfDockPotion?)dependencyObject.GetValue(FfDockPotionProperty);
    }

    public static void SetFfDockPotion(DependencyObject dependencyObject, FfDockPotion? value)
    {
        dependencyObject.SetValue(FfDockPotionProperty, value);
    }

    public static readonly DependencyProperty FfDockPotionProperty =
        DependencyProperty.RegisterAttached("FfDockPotion", typeof(FfDockPotion?),
            typeof(ControlBehavior), new PropertyMetadata(null));
}