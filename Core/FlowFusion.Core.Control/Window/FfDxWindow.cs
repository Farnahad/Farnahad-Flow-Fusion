using System.Windows;
using DevExpress.Xpf.Core;
using FarnahadFlowFusion.Core.Control.Core;
using Prism.Services.Dialogs;

namespace FarnahadFlowFusion.Core.Control.Window;

public class FfDxWindow : DXWindow, IDialogWindow
{
    public FfDxWindow()
    {
        ShowInTaskbar = false;
        SizeToContent = SizeToContent.WidthAndHeight;
        ResizeMode = ResizeMode.NoResize;
        Title = "Farnahad Flow Fusion";
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        ControlBehavior.SetFfFontFamily(this, FfFontFamily.SegoeUi);
        ControlBehavior.SetFfFontSize(this, FfFontSize.Normal);
    }

    public IDialogResult Result { get; set; }
}