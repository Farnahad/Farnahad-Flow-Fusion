namespace FlowFusion.Action.Main.Action;

public abstract class ExceptionAwareAction : IExceptionAwareAction
{
    public Guid Id { get; }

    public ExceptionAwareAction()
    {
        Id = Guid.NewGuid();
    }

    public abstract string Name { get; }
    public abstract Task Execute(SandBox sandBox);
    public Exception Exception { get; }
}