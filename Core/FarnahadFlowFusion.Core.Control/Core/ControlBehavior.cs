using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Grid;

namespace FarnahadFlowFusion.Core.Control.Core;

public static partial class ControlBehavior
{
    public static FffColumnWidth? GetFffColumnWidth(DependencyObject dependencyObject)
    {
        return (FffColumnWidth?)dependencyObject.GetValue(FffColumnWidthProperty);
    }

    public static void SetFffColumnWidth(DependencyObject obj, FffColumnWidth? value)
    {
        obj.SetValue(FffColumnWidthProperty, value);
    }

    public static readonly DependencyProperty FffColumnWidthProperty =
        DependencyProperty.RegisterAttached("FffColumnWidth", typeof(FffColumnWidth?),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(null, OnFffColumnWidthChanged));

    private static void OnFffColumnWidthChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var baseColumn = (BaseColumn)dependencyObject;

        var fffColumnWidth = (FffColumnWidth?)e.NewValue;

        if (fffColumnWidth != null)
        {
            if (fffColumnWidth == FffColumnWidth.Star1 || fffColumnWidth == FffColumnWidth.Star2 ||
                fffColumnWidth == FffColumnWidth.Star3)
            {
                baseColumn.Width = 5;
                baseColumn.Width = new GridColumnWidth((double)fffColumnWidth, GridColumnUnitType.Star);
                baseColumn.MinWidth = 5;
            }
            else if (fffColumnWidth == FffColumnWidth.Auto)
                baseColumn.MinWidth = 50;
            else
                baseColumn.MinWidth = (double)fffColumnWidth;
        }
    }


    public static FffControlWidth GetFffControlWidth(DependencyObject dependencyObject)
    {
        return (FffControlWidth)dependencyObject.GetValue(FffControlWidthProperty);
    }

    public static void SetFffControlWidth(DependencyObject dependencyObject, FffControlWidth value)
    {
        dependencyObject.SetValue(FffControlWidthProperty, value);
    }

    public static readonly DependencyProperty FffControlWidthProperty =
        DependencyProperty.RegisterAttached("FffControlWidth", typeof(FffControlWidth),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(FffControlWidth.Normal, OnFffControlWidthChanged));

    private static void OnFffControlWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var frameworkElement = (FrameworkElement)d;

        var fffControlWidth = (FffControlWidth)e.NewValue;

        if (fffControlWidth == FffControlWidth.Auto)
        {
            frameworkElement.Width = double.NaN;
            frameworkElement.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        else
            frameworkElement.Width = (double)fffControlWidth;
    }


    public static FffFontFamily GetFffFontFamily(DependencyObject dependencyObject)
    {
        return (FffFontFamily)dependencyObject.GetValue(FffFontFamilyProperty);
    }

    public static void SetFffFontFamily(DependencyObject obj, FffFontFamily value)
    {
        obj.SetValue(FffFontFamilyProperty, value);
    }

    public static readonly DependencyProperty FffFontFamilyProperty =
        DependencyProperty.RegisterAttached("FffFontFamily", typeof(FffFontFamily),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(FffFontFamily.SegoeUi, OnFffFontFamilyChanged));

    private static void OnFffFontFamilyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var control = (System.Windows.Controls.Control)dependencyObject;

        var fffFontFamily = (FffFontFamily)e.NewValue;

        if (DesignerProperties.GetIsInDesignMode(control) == false)
        {
            switch (fffFontFamily)
            {
                case FffFontFamily.SegoeUi:
                    control.FontFamily = new FontFamily(new Uri(
                        "pack://Application:,,,/Farnahad Flow Fusion;component/Fonts/"), "./#segoeui");
                    control.FontSize = 13;
                    break;
                case FffFontFamily.Tahoma:
                    break;
                case FffFontFamily.Roboto:
                    break;
            }
        }

        if (DesignerProperties.GetIsInDesignMode(control))
        {
            switch (fffFontFamily)
            {
                case FffFontFamily.SegoeUi:
                    control.FontFamily = new FontFamily("Segoe UI");
                    control.FontSize = 13;
                    break;
                case FffFontFamily.Tahoma:
                    break;
                case FffFontFamily.Roboto:
                    break;
            }
        }
    }


    public static FffFontSize GetFffFontSize(DependencyObject dependencyObject)
    {
        return (FffFontSize)dependencyObject.GetValue(FffFontSizeProperty);
    }

    public static void SetFffFontSize(DependencyObject dependencyObject, FffFontSize value)
    {
        dependencyObject.SetValue(FffFontSizeProperty, value);
    }

    public static readonly DependencyProperty FffFontSizeProperty =
        DependencyProperty.RegisterAttached("FffFontSize", typeof(FffFontSize),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(FffFontSize.Normal, OnFffFontSizeChanged));

    private static void OnFffFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (System.Windows.Controls.Control)d;

        var fffFontSize = (FffFontSize)e.NewValue;
        control.FontSize = (double)fffFontSize;
    }
}