using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class GetDefaultPrinter(IWorkstationService workstationService) : IAction
{
    public string Name => "Get default printer";

    public Variable PrinterName { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        PrinterName.Value = workstationService.GetDefaultPrinter();

        sandBox.SetVariable(PrinterName);
        await Task.CompletedTask;
    }
}