using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Grid;

namespace FlowFusion.Core.Control.Core;

public static partial class ControlBehavior
{
    public static FfColumnWidth? GetFfColumnWidth(DependencyObject dependencyObject)
    {
        return (FfColumnWidth?)dependencyObject.GetValue(FfColumnWidthProperty);
    }

    public static void SetFfColumnWidth(DependencyObject obj, FfColumnWidth? value)
    {
        obj.SetValue(FfColumnWidthProperty, value);
    }

    public static readonly DependencyProperty FfColumnWidthProperty =
        DependencyProperty.RegisterAttached("FfColumnWidth", typeof(FfColumnWidth?),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(null, OnFfColumnWidthChanged));

    private static void OnFfColumnWidthChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var baseColumn = (BaseColumn)dependencyObject;

        var ffColumnWidth = (FfColumnWidth?)e.NewValue;

        if (ffColumnWidth != null)
        {
            if (ffColumnWidth == FfColumnWidth.Star1 || ffColumnWidth == FfColumnWidth.Star2 ||
                ffColumnWidth == FfColumnWidth.Star3)
            {
                baseColumn.Width = 5;
                baseColumn.Width = new GridColumnWidth((double)ffColumnWidth, GridColumnUnitType.Star);
                baseColumn.MinWidth = 5;
            }
            else if (ffColumnWidth == FfColumnWidth.Auto)
                baseColumn.MinWidth = 50;
            else
                baseColumn.MinWidth = (double)ffColumnWidth;
        }
    }


    public static FfControlWidth GetFfControlWidth(DependencyObject dependencyObject)
    {
        return (FfControlWidth)dependencyObject.GetValue(FfControlWidthProperty);
    }

    public static void SetFfControlWidth(DependencyObject dependencyObject, FfControlWidth value)
    {
        dependencyObject.SetValue(FfControlWidthProperty, value);
    }

    public static readonly DependencyProperty FfControlWidthProperty =
        DependencyProperty.RegisterAttached("FfControlWidth", typeof(FfControlWidth),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(FfControlWidth.Normal, OnFfControlWidthChanged));

    private static void OnFfControlWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var frameworkElement = (FrameworkElement)d;

        var ffControlWidth = (FfControlWidth)e.NewValue;

        if (ffControlWidth == FfControlWidth.Auto)
        {
            frameworkElement.Width = double.NaN;
            frameworkElement.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        else
            frameworkElement.Width = (double)ffControlWidth;
    }


    public static FfFontFamily GetFfFontFamily(DependencyObject dependencyObject)
    {
        return (FfFontFamily)dependencyObject.GetValue(FfFontFamilyProperty);
    }

    public static void SetFfFontFamily(DependencyObject obj, FfFontFamily value)
    {
        obj.SetValue(FfFontFamilyProperty, value);
    }

    public static readonly DependencyProperty FfFontFamilyProperty =
        DependencyProperty.RegisterAttached("FfFontFamily", typeof(FfFontFamily),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(FfFontFamily.SegoeUi, OnFfFontFamilyChanged));

    private static void OnFfFontFamilyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var control = (System.Windows.Controls.Control)dependencyObject;

        var ffFontFamily = (FfFontFamily)e.NewValue;

        if (DesignerProperties.GetIsInDesignMode(control) == false)
        {
            switch (ffFontFamily)
            {
                case FfFontFamily.SegoeUi:
                    control.FontFamily = new FontFamily(new Uri(
                        "pack://Application:,,,/Farnahad Flow Fusion;component/Fonts/"), "./#segoeui");
                    control.FontSize = 13;
                    break;
                case FfFontFamily.Tahoma:
                    break;
                case FfFontFamily.Roboto:
                    break;
            }
        }

        if (DesignerProperties.GetIsInDesignMode(control))
        {
            switch (ffFontFamily)
            {
                case FfFontFamily.SegoeUi:
                    control.FontFamily = new FontFamily("Segoe UI");
                    control.FontSize = 13;
                    break;
                case FfFontFamily.Tahoma:
                    break;
                case FfFontFamily.Roboto:
                    break;
            }
        }
    }


    public static FfFontSize GetFfFontSize(DependencyObject dependencyObject)
    {
        return (FfFontSize)dependencyObject.GetValue(FfFontSizeProperty);
    }

    public static void SetFfFontSize(DependencyObject dependencyObject, FfFontSize value)
    {
        dependencyObject.SetValue(FfFontSizeProperty, value);
    }

    public static readonly DependencyProperty FfFontSizeProperty =
        DependencyProperty.RegisterAttached("FfFontSize", typeof(FfFontSize),
            typeof(DockLayout.ControlBehavior), new PropertyMetadata(FfFontSize.Normal, OnFfFontSizeChanged));

    private static void OnFfFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (System.Windows.Controls.Control)d;

        var ffFontSize = (FfFontSize)e.NewValue;
        control.FontSize = (double)ffFontSize;
    }
}