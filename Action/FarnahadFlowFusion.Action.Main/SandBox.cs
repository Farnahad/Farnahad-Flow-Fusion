using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Main;

public class SandBox
{
    private readonly List<Variable.Variable> _variables;

    public WorkFlow WorkFlow { get; set; }
    public IAction CurrentAction { get; set; }
    public SandBoxStatus SandBoxStatus { get; set; }
    public Exception Exception { get; set; }

    public SandBox(WorkFlow workFlow)
    {
        WorkFlow = workFlow;
        InitialWorkFlow();
        _variables = new List<Variable.Variable>();
    }

    private void InitialWorkFlow()
    {
        foreach (var inputVariable in WorkFlow.InputVariables)
        {
                
        }

        foreach (var outputVariable in WorkFlow.OutputVariables)
        {
            
        }
    }

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

    public T GetValue<T>(ActionInput actionInput)
    {
        var realVariable = _variables.FirstOrDefault(item => item.Name == variable.Name);

        if (realVariable != null)
            return realVariable.Value;

        return default;
    }

    public void SetValue(Variable.Variable variable)
    {
        if (variable.Enable)
        {
            var realVariable = _variables.FirstOrDefault(item => item.Name == variable.Name);

            if (variable != null)
            {

            }
            else
            {
                realVariable = new Variable.Variable();
            }
        }
    }

    public async Task<T> EvaluateActionInput<T>(SandBox sandBox, ActionInput actionInput)
    {
        return Task.FromResult(new T());
    }
}