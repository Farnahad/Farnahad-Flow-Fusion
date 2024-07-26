using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Workstation.TakeScreenshotBase;

namespace FarnahadFlowFusion.Action.Workstation;

public class TakeScreenshot : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Take screenshot";

    public Capture Capture { get; set; }
    public SaveScreenshotTo SaveScreenshotTo { get; set; }
    public ActionInput ScreenToCapture { get; set; }

    public TakeScreenshot()
    {
        _cSharpService = new CSharpService();

        Capture = Capture.AllScreens;
        SaveScreenshotTo = SaveScreenshotTo.Clipboard;
        ScreenToCapture = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.Variables.Add(XXXXXXXXXXXX);
    }
}