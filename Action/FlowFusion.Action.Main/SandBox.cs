using FlowFusion.Action.Main.Action;
using System.Reflection.Emit;

namespace FlowFusion.Action.Main;

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

    public async Task<SandBox> Run(WorkFlow workFlow)
    {
        var index = 0;
        CurrentAction = workFlow.Actions[index];

        while (CurrentAction != null)
        {
            await CurrentAction.Execute(this);

            index++;
            if (workFlow.Actions.Count == index)
                break;

            CurrentAction = workFlow.Actions[index];
        }

        return this;
    }

    public T GetValue<T>(Variable.Variable variable)
    {
        var realVariable = _variables.FirstOrDefault(item => item.Name == variable.Name);

        if (realVariable != null)
            return (T)realVariable.Value;

        return default;
    }

    public void SetVariable(Variable.Variable variable)
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

    public async Task<T> EvaluateActionInput<T>(ActionInput actionInput)
    {
        return Task.FromResult(new T());
    }

    public void GoToLabel(string labelName)
    {
        var labelAction = WorkFlow.Actions.FirstOrDefault(item => item.Name == labelName);

        if (labelAction != null)
            CurrentAction = labelAction;
    }

    public void GoToBeggingOfBlock()
    {
    }

    public void GoToEndOfBlock()
    {
    }

    public void GoToNextAction()
    {
    }

    public void RepeatAction()
    {
    }

    public void ThrowError(Exception exception)
    {
    }

    public async Task Stop()
    {
        await Task.CompletedTask;
    }

    public void ExitSubflow()
    {
    }
}