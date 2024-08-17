using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;
using FlowFusion.Service.Workstation.Workstation.Base;

namespace FlowFusion.Action.Workstation;

public class TakeScreenshot(IWorkstationService workstationService) : IAction
{
    public string Name => "Take screenshot";

    public ScreenshotCapture Capture { get; set; } = ScreenshotCapture.AllScreens;
    public SaveScreenshotTo SaveScreenshotTo { get; set; } = SaveScreenshotTo.Clipboard;
    public ActionInput ScreenToCapture { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var screenToCaptureValue = await sandBox.EvaluateActionInput<int>(ScreenToCapture);

        workstationService.TakeScreenshot(Capture, SaveScreenshotTo, screenToCaptureValue);
    }
}