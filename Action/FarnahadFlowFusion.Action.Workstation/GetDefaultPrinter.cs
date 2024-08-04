using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Workstation;

public class GetDefaultPrinter : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Get default printer";

    public Variable PrinterName { get; set; }

    public GetDefaultPrinter()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();

        PrinterName = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        PrinterName.Value = _workstationService.GetDefaultPrinter();

        sandBox.Variables.Add(PrinterName);
    }
}