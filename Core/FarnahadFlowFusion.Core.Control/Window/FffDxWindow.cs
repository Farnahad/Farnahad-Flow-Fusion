using System.Windows;
using DevExpress.Xpf.Core;
using FarnahadFlowFusion.Core.Control.Core;
using Prism.Services.Dialogs;

namespace FarnahadFlowFusion.Core.Control.Window;

public class FffDxWindow : DXWindow, IDialogWindow
{
    public FffDxWindow()
    {
        ShowInTaskbar = false;
        SizeToContent = SizeToContent.WidthAndHeight;
        ResizeMode = ResizeMode.NoResize;
        Title = "Farnahad Flow Fusion";
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        ControlBehavior.SetFffFontFamily(this, FffFontFamily.SegoeUi);
        ControlBehavior.SetFffFontSize(this, FffFontSize.Normal);
    }

    public IDialogResult Result { get; set; }
}