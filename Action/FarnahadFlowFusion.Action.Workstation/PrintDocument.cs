using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

public class PrintDocument : IAction
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