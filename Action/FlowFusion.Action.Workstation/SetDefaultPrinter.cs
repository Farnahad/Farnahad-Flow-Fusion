using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

public class SetDefaultPrinter : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Set default printer";

    public ActionInput PrinterName { get; set; }

    public SetDefaultPrinter()
    {
        _workstationService = new WorkstationService();

        PrinterName = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var printerNameValue = await sandBox.EvaluateActionInput<string>(PrinterName);

        _workstationService.SetDefaultPrinter(printerNameValue);
    }
}