using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.System;

public class Ping : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Ping";

    public ActionInput HostName { get; set; }
    public ActionInput TimeOut { get; set; }
    public Variable PingResult { get; set; }
    public Variable RoundTripTime { get; set; }

    public Ping()
    {
        _cSharpService = new CSharpService();

        HostName = new ActionInput();
        TimeOut = new ActionInput();
        PingResult = new Variable();
        RoundTripTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var hostNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, HostName);
        var timeOutValue = await _cSharpService.EvaluateActionInput<int>(sandBox, TimeOut);

        var ping = new global::System.Net.NetworkInformation.Ping();
        var reply = await ping.SendPingAsync(hostNameValue, timeOutValue * 1000);

        if (PingResult != null)
            PingResult.Value = reply.Status.ToString();

        if (RoundTripTime != null)
            RoundTripTime.Value = reply.RoundtripTime.ToString();

        sandBox.Variables.Add(PingResult);
        sandBox.Variables.Add(RoundTripTime);
    }
}