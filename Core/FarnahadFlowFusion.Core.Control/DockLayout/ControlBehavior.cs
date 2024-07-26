using System.Windows;

namespace FarnahadFlowFusion.Core.Control.DockLayout;

public static partial class ControlBehavior
{
    public static FffDockType? GetFffDockType(DependencyObject dependencyObject)
    {
        return (FffDockType?)dependencyObject.GetValue(FffDockTypeProperty);
    }

    public static void SetFffDockType(DependencyObject dependencyObject, FffDockType? value)
    {
        dependencyObject.SetValue(FffDockTypeProperty, value);
    }

    public static readonly DependencyProperty FffDockTypeProperty =
        DependencyProperty.RegisterAttached("FffDockType", typeof(FffDockType?),
            typeof(ControlBehavior), new PropertyMetadata(null));


    public static FffDockPotion? GetFffDockPotion(DependencyObject dependencyObject)
    {
        return (FffDockPotion?)dependencyObject.GetValue(FffDockPotionProperty);
    }

    public static void SetFffDockPotion(DependencyObject dependencyObject, FffDockPotion? value)
    {
        dependencyObject.SetValue(FffDockPotionProperty, value);
    }

    public static readonly DependencyProperty FffDockPotionProperty =
        DependencyProperty.RegisterAttached("FffDockPotion", typeof(FffDockPotion?),
            typeof(ControlBehavior), new PropertyMetadata(null));
}