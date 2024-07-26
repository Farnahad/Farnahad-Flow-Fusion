namespace FarnahadFlowFusion.Action.Main;

public class SandBox
{
    public SandBox(WorkFlow workFlow)
    {
        WorkFlow = workFlow;
        Variables = new List<Variable>();
        WorkFlow.Variables.ForEach(
            item => Variables.Add(new Variable(item.Name, item.Value)));
    }

    public WorkFlow WorkFlow { get; set; }
    public IAction CurrentAction { get; set; }
    public List<Variable> Variables { get; set; }
    public SandBoxStatus SandBoxStatus { get; set; }
    public Exception Exception { get; set; }

    public async Task<SandBox> Run()
    {
        var index = 0;
        CurrentAction = WorkFlow.Actions[index];

        while (CurrentAction != null)
        {
            await CurrentAction.Execute(this);

            index++;
            if (WorkFlow.Actions.Count == index)
                break;

            CurrentAction = WorkFlow.Actions[index];
        }

        return this;
    }

    public object GetVariableValue(string name)
    {
        var variable = Variables.FirstOrDefault(item => item.Name == name);

        if (variable != null)
            return variable.Value;

        return null;
    }

    public object GetListVariableValue(string name)
    {
        var listVariable = (ListVariable)Variables.FirstOrDefault(
            item => item.Name == name);

        if (listVariable != null)
            return listVariable.Value;

        return null;
    }
}