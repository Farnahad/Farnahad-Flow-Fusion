using FlowFusion.Action.DateTime.SubtractDatesBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.DateTime;

public class SubtractDates : IAction
{
    public string Name => "Subtract dates";

    public ActionInput FromDate { get; set; }
    public ActionInput SubtractDate { get; set; }
    public GetDifferenceIn GetDifferenceIn { get; set; }
    public Variable TimeDifference { get; set; }

    public SubtractDates()
    {
        FromDate = new ActionInput();
        SubtractDate = new ActionInput();
        GetDifferenceIn = GetDifferenceIn.Days;
        TimeDifference = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fromDateValue = await sandBox.EvaluateActionInput<global::System.DateTime>(FromDate);
        var subtractDateValue = await sandBox.EvaluateActionInput<global::System.DateTime>(SubtractDate);

        var dateTimeResult = fromDateValue - subtractDateValue;

        TimeDifference.Value = GetDifferenceIn switch
        {
            GetDifferenceIn.Days => dateTimeResult.Days,
            GetDifferenceIn.Hours => dateTimeResult.Hours,
            GetDifferenceIn.Minutes => dateTimeResult.Minutes,
            GetDifferenceIn.Seconds => dateTimeResult.Seconds,
            _ => TimeDifference.Value
        };

        sandBox.SetVariable(TimeDifference);
    }
}