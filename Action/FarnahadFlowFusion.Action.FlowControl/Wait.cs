using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.FlowControl;

public class Wait : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Wait";

    public ActionInput Duration { get; set; }

    public Wait()
    {
        _cSharpService = new CSharpService();

        Duration = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var durationValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Duration);
        await Task.Delay(TimeSpan.FromSeconds(durationValue));
    }
}