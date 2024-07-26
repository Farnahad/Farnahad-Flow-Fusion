using System.Windows;
using DevExpress.Xpf.Ribbon;
using FarnahadFlowFusion.Core.Control.Core;
using Prism.Services.Dialogs;

namespace FarnahadFlowFusion.Core.Control.Window;

public class FffDxRibbonWindow : DXRibbonWindow, IDialogWindow
{
    public FffDxRibbonWindow()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        WindowState = WindowState.Maximized;

        ControlBehavior.SetFffFontFamily(this, FffFontFamily.SegoeUi);
        ControlBehavior.SetFffFontSize(this, FffFontSize.Normal);
    }

    public IDialogResult Result { get; set; }
}