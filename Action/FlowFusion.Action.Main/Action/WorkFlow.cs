using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Main.Action;

public class WorkFlow
{
    public WorkFlow()
    {
        InputVariables = new List<InputVariable>();
        OutputVariables = new List<OutputVariable>();
        Actions = new List<IAction>();
    }

    public List<InputVariable> InputVariables { get; set; }
    public List<OutputVariable> OutputVariables { get; set; }
    public List<IAction> Actions { get; set; }
}