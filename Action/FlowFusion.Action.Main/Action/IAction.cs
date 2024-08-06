namespace FlowFusion.Action.Main.Action;

public interface IAction
{
    string Name { get; }
    Task Execute(SandBox sandBox);
}