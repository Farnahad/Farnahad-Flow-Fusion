using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Workstation.ShowDesktopBase;

namespace FlowFusion.Action.Workstation;

public class ShowDesktop : IAction //XXXXXXXXXXXX
{
    public string Name => "Show desktop";

    public Operation Operation { get; set; }

    public ShowDesktop()
    {

        Operation = Operation.MinimizeAllWindowsShowDesktop;
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.SetVariable(XXXXXXXXXXXX);
    }
}