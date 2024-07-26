using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Workstation;

public class PrintDocument : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Print document";

    public ActionInput DocumentToPrint { get; set; }

    public PrintDocument()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();

        DocumentToPrint = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var documentToPrintValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DocumentToPrint);

        _workstationService.SetDefaultPrinter(documentToPrintValue);
    }
}