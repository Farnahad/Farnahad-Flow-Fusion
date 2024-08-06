using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.System;

public class Ping : IAction
{
    public string Name => "Ping";

    public ActionInput HostName { get; set; }
    public ActionInput TimeOut { get; set; }
    public Variable PingResult { get; set; }
    public Variable RoundTripTime { get; set; }

    public Ping()
    {
        HostName = new ActionInput();
        TimeOut = new ActionInput();
        PingResult = new Variable();
        RoundTripTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var hostNameValue = await sandBox.EvaluateActionInput<string>(HostName);
        var timeOutValue = await sandBox.EvaluateActionInput<int>(TimeOut);

        var ping = new global::System.Net.NetworkInformation.Ping();
        var reply = await ping.SendPingAsync(hostNameValue, timeOutValue * 1000);

        if (PingResult != null)
            PingResult.Value = reply.Status.ToString();

        if (RoundTripTime != null)
            RoundTripTime.Value = reply.RoundtripTime.ToString();

        sandBox.SetVariable(PingResult);
        sandBox.SetVariable(RoundTripTime);
    }
}