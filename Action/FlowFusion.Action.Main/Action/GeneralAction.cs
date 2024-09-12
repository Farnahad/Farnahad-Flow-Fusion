namespace FlowFusion.Action.Main.Action;

public abstract class GeneralAction : IAction
{
    public Guid Id { get; }

    public GeneralAction()
    {
        Id = Guid.NewGuid();
    }

    public abstract string Name { get; }
    public abstract Task Execute(SandBox sandBox);
}