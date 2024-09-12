using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.System.System;

namespace FlowFusion.Action.System;

public class Ping(ISystemService systemService) : IAction
{
    public string Name => "Ping";

    public ActionInput HostName { get; set; } = new();
    public ActionInput TimeOut { get; set; } = new();
    public Variable PingResult { get; set; } = new();
    public Variable RoundTripTime { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var hostNameValue = await sandBox.EvaluateActionInput<string>(HostName);
        var timeOutValue = await sandBox.EvaluateActionInput<int>(TimeOut);

        var pingResult = await systemService.Ping(hostNameValue, timeOutValue);
        PingResult.Value = pingResult.Item1;
        RoundTripTime.Value = pingResult.Item2;

        sandBox.SetVariable(PingResult);
        sandBox.SetVariable(RoundTripTime);
    }
}