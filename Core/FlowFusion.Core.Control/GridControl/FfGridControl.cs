using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;

namespace FlowFusion.Core.Control.GridControl;

public abstract class FfGridControl : DevExpress.Xpf.Grid.GridControl
{
    public FfGridControl()
    {
        SelectionMode = MultiSelectMode.None;

        var ffTableView = new FfTableView();
        View = ffTableView;
    }


    public static readonly DependencyProperty MouseDoubleClickCommandProperty = DependencyProperty.Register(
        nameof(MouseDoubleClickCommand), typeof(ICommand), typeof(FfGridControl),
        new PropertyMetadata(null));

    public ICommand MouseDoubleClickCommand
    {
        get => (ICommand)GetValue(MouseDoubleClickCommandProperty);
        set => SetValue(MouseDoubleClickCommandProperty, value);
    }
}