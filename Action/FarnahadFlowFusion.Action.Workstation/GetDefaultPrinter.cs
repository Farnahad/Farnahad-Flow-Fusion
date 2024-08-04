using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

public class GetDefaultPrinter : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Get default printer";

    public Variable PrinterName { get; set; }

    public GetDefaultPrinter()
    {
        _workstationService = new WorkstationService();

        PrinterName = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        PrinterName.Value = _workstationService.GetDefaultPrinter();

        sandBox.SetVariable(PrinterName);
    }
}