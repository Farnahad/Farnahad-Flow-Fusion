using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class SetDefaultPrinter(IWorkstationService workstationService) : IAction
{
    public string Name => "Set default printer";

    public ActionInput PrinterName { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var printerNameValue = await sandBox.EvaluateActionInput<string>(PrinterName);

        workstationService.SetDefaultPrinter(printerNameValue);
    }
}