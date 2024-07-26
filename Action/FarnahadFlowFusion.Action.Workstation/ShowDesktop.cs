using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Workstation.ShowDesktopBase;

namespace FarnahadFlowFusion.Action.Workstation;

public class ShowDesktop : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Show desktop";

    public Operation Operation { get; set; }

    public ShowDesktop()
    {
        _cSharpService = new CSharpService();

        Operation = Operation.MinimizeAllWindowsShowDesktop;
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.Variables.Add(XXXXXXXXXXXX);
    }
}