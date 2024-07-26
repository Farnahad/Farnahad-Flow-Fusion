using System.Windows;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadFlowFusion.Core.Control.Layout;

public class FffAutoLayoutItem : LayoutItem
{
    public FffAutoLayoutItem()
    {
        AddColonToLabel = true;
    }


    public object EditValue
    {
        get => (object)GetValue(EditValueProperty);
        set => SetValue(EditValueProperty, value);
    }

    public static readonly DependencyProperty EditValueProperty =
        DependencyProperty.Register(nameof(EditValue), typeof(object),
            typeof(FffAutoLayoutItem), new PropertyMetadata(null));
}