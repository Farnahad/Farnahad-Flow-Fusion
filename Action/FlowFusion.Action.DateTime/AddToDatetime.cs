using FarnahadFlowFusion.Action.DateTime.AddToDatetimeBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.DateTime;

public class AddToDatetime : IAction
{
    public string Name => "Add to datetime";

    public ActionInput Datetime { get; set; }
    public ActionInput Add { get; set; }
    public TimeUnit TimeUnit { get; set; }
    public Variable ResultedDate { get; set; }

    public AddToDatetime()
    {
        Datetime = new ActionInput();
        Add = new ActionInput();
        TimeUnit = TimeUnit.Seconds;
        ResultedDate = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var datetimeValue = await sandBox.EvaluateActionInput<global::System.DateTime>(Datetime);
        var addValue = await sandBox.EvaluateActionInput<double>(Add);

        ResultedDate.Value = TimeUnit switch
        {
            TimeUnit.Days => datetimeValue.AddDays(addValue),
            TimeUnit.Hours => datetimeValue.AddHours(addValue),
            TimeUnit.Minutes => datetimeValue.AddMinutes(addValue),
            TimeUnit.Months => datetimeValue.AddMonths((int)addValue),
            TimeUnit.Seconds => datetimeValue.AddSeconds(addValue),
            TimeUnit.Years => datetimeValue.AddYears((int)addValue),
            _ => ResultedDate.Value
        };

        sandBox.SetVariable(ResultedDate);
    }
}