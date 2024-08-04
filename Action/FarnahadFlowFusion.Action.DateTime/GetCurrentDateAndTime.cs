using FarnahadFlowFusion.Action.DateTime.GetCurrentDateAndTimeBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using TimeZone = FarnahadFlowFusion.Action.DateTime.GetCurrentDateAndTimeBase.TimeZone;

namespace FarnahadFlowFusion.Action.DateTime;

public class GetCurrentDateAndTime : IAction
{
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
        Retrieve = Retrieve.CurrentDateAndTime;
        TimeZone = TimeZone.SystemTimeZone;
        CurrentDateTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var countryRegionValue = await sandBox.EvaluateActionInput<int>(CountryRegion);
        var windowsTimeZoneValue = await sandBox.EvaluateActionInput<string>(WindowsTimeZone);
        var offsetValue = await sandBox.EvaluateActionInput<int>(Offset);

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

        sandBox.SetVariable(CurrentDateTime);
    }
}