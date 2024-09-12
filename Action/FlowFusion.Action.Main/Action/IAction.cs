namespace FlowFusion.Action.Main.Action;

public interface IAction
{
    Guid Id { get; }
    string Name { get; }
    Task Execute(SandBox sandBox);
}