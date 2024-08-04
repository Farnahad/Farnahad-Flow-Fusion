using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Workstation.TakeScreenshotBase;

namespace FarnahadFlowFusion.Action.Workstation;

public class TakeScreenshot : IAction
{
    public string Name => "Take screenshot";

    public Capture Capture { get; set; }
    public SaveScreenshotTo SaveScreenshotTo { get; set; }
    public ActionInput ScreenToCapture { get; set; }

    public TakeScreenshot()
    {
        Capture = Capture.AllScreens;
        SaveScreenshotTo = SaveScreenshotTo.Clipboard;
        ScreenToCapture = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.SetVariable(XXXXXXXXXXXX);
    }
}