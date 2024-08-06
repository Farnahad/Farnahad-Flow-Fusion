using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Workstation.TakeScreenshotBase;

namespace FlowFusion.Action.Workstation;

public class TakeScreenshot : IAction //XXXXXXXXXXXX
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