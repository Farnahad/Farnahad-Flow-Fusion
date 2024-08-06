using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class PrintDocument : IAction //XXXXXXXXXXXX
{
    private readonly WorkstationService _workstationService;

    public string Name => "Print document";

    public ActionInput DocumentToPrint { get; set; }

    public PrintDocument()
    {
        _workstationService = new WorkstationService();

        DocumentToPrint = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var documentToPrintValue = await sandBox.EvaluateActionInput<string>(DocumentToPrint);

        _workstationService.SetDefaultPrinter(documentToPrintValue);
    }
}