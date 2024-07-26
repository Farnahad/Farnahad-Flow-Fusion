using FarnahadFlowFusion.Action.DateTime.SubtractDatesBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.DateTime;

public class SubtractDates : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Subtract dates";

    public ActionInput FromDate { get; set; }
    public ActionInput SubtractDate { get; set; }
    public GetDifferenceIn GetDifferenceIn { get; set; }
    public Variable TimeDifference { get; set; }

    public SubtractDates()
    {
        _cSharpService = new CSharpService();

        FromDate = new ActionInput();
        SubtractDate = new ActionInput();
        GetDifferenceIn = GetDifferenceIn.Days;
        TimeDifference = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fromDateValue = await _cSharpService.EvaluateActionInput<global::System.DateTime>(sandBox, FromDate);
        var subtractDateValue = await _cSharpService.EvaluateActionInput<global::System.DateTime>(sandBox, SubtractDate);

        var dateTimeResult = fromDateValue - subtractDateValue;

        TimeDifference.Value = GetDifferenceIn switch
        {
            GetDifferenceIn.Days => dateTimeResult.Days,
            GetDifferenceIn.Hours => dateTimeResult.Hours,
            GetDifferenceIn.Minutes => dateTimeResult.Minutes,
            GetDifferenceIn.Seconds => dateTimeResult.Seconds,
            _ => TimeDifference.Value
        };

        sandBox.Variables.Add(TimeDifference);
    }
}