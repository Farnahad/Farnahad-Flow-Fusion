using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;

namespace FarnahadFlowFusion.Core.Control.GridControl;

public abstract class FffGridControl : DevExpress.Xpf.Grid.GridControl
{
    public FffGridControl()
    {
        SelectionMode = MultiSelectMode.None;

        var fffTableView = new FffTableView();
        View = fffTableView;
    }


    public static readonly DependencyProperty MouseDoubleClickCommandProperty = DependencyProperty.Register(
        nameof(MouseDoubleClickCommand), typeof(ICommand), typeof(FffGridControl),
        new PropertyMetadata(null));

    public ICommand MouseDoubleClickCommand
    {
        get => (ICommand)GetValue(MouseDoubleClickCommandProperty);
        set => SetValue(MouseDoubleClickCommandProperty, value);
    }
}