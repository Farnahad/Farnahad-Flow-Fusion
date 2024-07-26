using FarnahadFlowFusion.Action.DateTime.GetCurrentDateAndTimeBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;
using TimeZone = FarnahadFlowFusion.Action.DateTime.GetCurrentDateAndTimeBase.TimeZone;

namespace FarnahadFlowFusion.Action.DateTime;

public class GetCurrentDateAndTime : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get current date and time";

    public Retrieve Retrieve { get; set; }
    public TimeZone TimeZone { get; set; }
    public ActionInput CountryRegion { get; set; }
    public ActionInput WindowsTimeZone { get; set; }
    public InputType InputType { get; set; }
    public ActionInput Offset { get; set; }
    public Variable CurrentDateTime { get; set; }

    public GetCurrentDateAndTime()
    {
        _cSharpService = new CSharpService();

        Retrieve = Retrieve.CurrentDateAndTime;
        TimeZone = TimeZone.SystemTimeZone;
        CurrentDateTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var countryRegionValue = await _cSharpService.EvaluateActionInput<int>(sandBox, CountryRegion);
        var windowsTimeZoneValue = await _cSharpService.EvaluateActionInput<string>(sandBox, WindowsTimeZone);
        var offsetValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Offset);

        var currentDateTime = new global::System.DateTime();

        switch (InputType)
        {
            case InputType.Offset:
                currentDateTime = global::System.DateTime.UtcNow.AddHours(offsetValue);
                break;

            case InputType.WindowsTimeZone:
                currentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                    global::System.DateTime.UtcNow, windowsTimeZoneValue);
                break;
        }

        CurrentDateTime.Value = Retrieve == Retrieve.CurrentDateAndTime ? currentDateTime : currentDateTime.Date;

        sandBox.Variables.Add(CurrentDateTime);
    }
}