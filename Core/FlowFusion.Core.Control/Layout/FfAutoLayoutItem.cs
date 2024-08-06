using System.Windows;
using DevExpress.Xpf.LayoutControl;

namespace FlowFusion.Core.Control.Layout;

public class FfAutoLayoutItem : LayoutItem
{
    public FfAutoLayoutItem()
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
            typeof(FfAutoLayoutItem), new PropertyMetadata(null));
}