namespace FarnahadFlowFusion.Action.Main;

public class ActionInput
{
    public string Formula { get; set; }

    public object Evaluate(SandBox sandBox)
    {
        return Formula;
    }
}