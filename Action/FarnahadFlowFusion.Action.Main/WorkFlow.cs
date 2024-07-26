namespace FarnahadFlowFusion.Action.Main;

public class WorkFlow
{
    public WorkFlow()
    {
        Actions = new List<IAction>();
        Variables = new List<Variable>();
    }

    public List<IAction> Actions { get; set; }
    public List<Variable> Variables { get; set; }
}