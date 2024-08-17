using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class PrintDocument(IWorkstationService workstationService) : IAction
{
    public string Name => "Print document";

    public ActionInput DocumentToPrint { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var documentToPrintValue = await sandBox.EvaluateActionInput<string>(DocumentToPrint);

        workstationService.PrintDocument(documentToPrintValue);
    }
}