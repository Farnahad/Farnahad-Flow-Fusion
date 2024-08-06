using System.Windows;
using DevExpress.Xpf.Ribbon;
using FlowFusion.Core.Control.Core;
using Prism.Services.Dialogs;

namespace FlowFusion.Core.Control.Window;

public class FfDxRibbonWindow : DXRibbonWindow, IDialogWindow
{
    public FfDxRibbonWindow()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        WindowState = WindowState.Maximized;

        ControlBehavior.SetFfFontFamily(this, FfFontFamily.SegoeUi);
        ControlBehavior.SetFfFontSize(this, FfFontSize.Normal);
    }

    public IDialogResult Result { get; set; }
}