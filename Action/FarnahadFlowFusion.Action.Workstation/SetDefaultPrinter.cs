using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Workstation;

public class SetDefaultPrinter : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Set default printer";

    public ActionInput PrinterName { get; set; }

    public SetDefaultPrinter()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();

        PrinterName = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var printerNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, PrinterName);

        _workstationService.SetDefaultPrinter(printerNameValue);
    }
}