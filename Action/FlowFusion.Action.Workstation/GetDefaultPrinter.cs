using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class GetDefaultPrinter : IAction //XXXXXXXXXXXX
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